using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;
using System.Web.Http.Results;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using WebApplication2017_WebApi_JWT.Repository;
using WebApplication2017_WebApi_JWT.Models;
namespace WebApplication2017_WebApi_JWT.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private JwtService _jwtService = new JwtService();

        /// <summary>
        /// 登入
        /// </summary>
        /// <response code="200">OK</response> 
        /// <response code="404">NotFound</response> 
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login([FromBody] LoginData model)
        {
            // 假設的使用者驗證過程
            if (this.CheckUser(model.Username, model.Password))
            {
                string role = model.Username.Equals("Brian") ? "Admin" : "user";
                var token = _jwtService.GenerateToken(model.Username, role);
               
                return Ok(new
                {
                    message = "成功",
                    username = model.Username,
                    department = model.Department, // 如果需要回傳部門資訊
                    token           
                });

            }

            // 如果使用者驗證失敗，返回NotFound
            return Content(HttpStatusCode.NotFound, new
            {
                message = "查無此人，可能帳號或密碼有錯！",
                username = model.Username
            });
        }
        bool CheckUser(string username, string password)
        {
            // should check in the LDAP
            return true;
        }
        /// <summary>
        /// 取得過期時間
        /// </summary>
        /// <response code="200">OK</response> 
        /// <response code="400">Token is invalid.</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <returns></returns>
        [HttpGet]
        [Route("GetValidTime")]
        [CustomAuthorize(Roles = "Admin")]
        public IHttpActionResult GetValidTime()
        {
            var token = Request.Headers.Authorization.Parameter;
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken != null)
            {
                var validTo = jwtToken.ValidTo;
                // 將過期時間轉換為本地時間（可選）
                return Ok(new { validTime = validTo.ToLocalTime() });
            }

            // 如果無法讀取Token信息，返回錯誤響應
            return BadRequest("Token is invalid.");
        }
        [HttpGet]
        [Route("Test")]
        [CustomAuthorize]
        public IHttpActionResult Test()
        {
            var token = Request.Headers.Authorization.Parameter;
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken != null)
            {
                var validTo = jwtToken.ValidTo;
                // 將過期時間轉換為本地時間（可選）
                return Ok(new { validTime = validTo.ToLocalTime() });
            }

            // 如果無法讀取Token信息，返回錯誤響應
            return BadRequest("Token is invalid.");
        }
    }
}