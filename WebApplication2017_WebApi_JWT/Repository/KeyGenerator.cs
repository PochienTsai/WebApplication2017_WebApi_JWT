using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
namespace WebApplication2017_WebApi_JWT.Repository
{
    public class KeyGenerator
    {
        public static string GenerateKey(int size) // size推薦128以上
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }
    }
}