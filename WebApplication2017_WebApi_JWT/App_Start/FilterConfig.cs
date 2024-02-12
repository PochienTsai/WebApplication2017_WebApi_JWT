using System.Web;
using System.Web.Mvc;

namespace WebApplication2017_WebApi_JWT
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
