using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace WebApplication2017_WebApi_JWT.Repository
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (actionContext.RequestContext.Principal != null && actionContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                // 身份驗證通過但授權未通過
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent("Access Denied: You do not have permission to access this resource.")
                };
                actionContext.Response = response;
            }
            else
            {
                // 身份驗證未通過
                var response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent("Unauthorized: Authentication is required and has failed or has not yet been provided.")
                };
                actionContext.Response = response;
            }
        }
    }
}