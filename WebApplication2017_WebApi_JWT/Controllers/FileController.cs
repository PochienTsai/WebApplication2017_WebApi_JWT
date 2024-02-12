using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication2017_WebApi_JWT.Controllers
{
    [RoutePrefix("api/File")]
    public class FileController : ApiController
    {

        /// <summary>
        /// 上傳檔案
        /// </summary>
        /// <response code="200">OK</response> 
        /// <response code="500">InternalServerError</response> 
        /// <returns></returns>
        [HttpPost]
        [Route("UploadFile")]
        public async Task<IHttpActionResult> UploadFile()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var root = HttpContext.Current.Server.MapPath("~/App_Data/uploads");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (var file in provider.FileData)
                {
                    var name = file.Headers.ContentDisposition.FileName.Trim('"');
                    var localFileName = file.LocalFileName;
                    var filePath = Path.Combine(root, name);

                    // 可以在此處理檔案，如移動到其他位置或其他操作
                    File.Move(localFileName, filePath);
                }

                return Ok("檔案上傳成功");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// 下載檔案
        /// </summary>
        /// <param name="fileName"></param>
        /// <response code="200">OK</response> 
        /// <response code="404">NotFound</response> 
        /// <returns></returns>
        [HttpGet]
        [Route("DownloadFile")]
        //[Route("DownloadFile/{fileName}")]
        public HttpResponseMessage DownloadFile(string fileName)
        {
            var filePath = HttpContext.Current.Server.MapPath($"~/App_Data/uploads/{fileName}");
            if (!File.Exists(filePath))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(File.ReadAllBytes(filePath))
            };
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

            return response;
        }
        //// GET: api/File
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/File/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/File
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/File/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/File/5
        //public void Delete(int id)
        //{
        //}
    }
}
