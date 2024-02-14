using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Configuration;

namespace WebApplication2017_WebApi_JWT.Repository
{
    public class JwtService
    {
        private readonly string _secretKey;
        private readonly string _issuer = "your_issuer";
        private readonly string _audience = "your_audience";

        public JwtService()
        {
            _secretKey = ConfigurationManager.AppSettings["JWTSecretKey"];
        }

        public string GenerateToken(string userName, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.Role, role)
        };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}