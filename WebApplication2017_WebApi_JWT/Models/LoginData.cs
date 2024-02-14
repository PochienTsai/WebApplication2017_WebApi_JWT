using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2017_WebApi_JWT.Models
{
    public class LoginData
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Department { get; set; }
        public string AdName { get; set; }
    }
}