using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2017_WebApi_JWT.Models;

namespace WebApplication2017_WebApi_JWT.Controllers
{
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {   
        //此為微軟提供的範例
        #region  // 加入預設值（基本資料）
        // 程式沒有防呆，ID數字請勿重複！
        Product[] products = new Product[]
        {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M },
            new Product { Id = 4, Name = "張中文", Category = "Toys", Price = 5 },
            new Product { Id = 5, Name = "李法文", Category = "Hardware", Price = 6M },
            new Product { Id = 6, Name = "許功蓋(PHP)", Category = "Groceries", Price = 7 }
        };
        #endregion
        public IHttpActionResult GetAllProducts()
        {   // 傳回值 -- (3)  IHttpActionResult
            return Ok(products);

            // returns an HttpStatusCode.OK   // https://docs.microsoft.com/zh-tw/previous-versions/aspnet/dn308866%28v%3dvs.118%29
            // Http狀態碼(status code),常見的如下
            //200 OK
            //400 Bad Request 收到無效語法,而無法理解請求
            //404 Not Found 伺服器找不到請求的資源
            //500 Internal Server Error
        }
        // GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}