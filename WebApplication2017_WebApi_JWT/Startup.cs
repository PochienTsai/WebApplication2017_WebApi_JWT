using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System.Configuration;
using System.Text;

[assembly: OwinStartup(typeof(WebApplication2017_WebApi_JWT.Startup))]

namespace WebApplication2017_WebApi_JWT
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 如需如何設定應用程式的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCors(CorsOptions.AllowAll); // 啟用CORS
            // 在JWT（JSON Web Tokens）的上下文中，issuer（發行者）和audience（觀眾）是兩個重要的聲明（claims），它們用於增加Token的安全性和目的性。讓我們來具體看看它們各自的意義和一般應如何填寫：

            //Issuer(iss)
            //意義：issuer聲明標識了發行JWT的實體，通常是指你的服務或應用。這可以是一個網址或一個簡短的標識符，其目的是讓Token的接收者能夠識別Token的發行來源。
            //填寫建議：你應該使用一個唯一標識你的服務或應用的字符串。如果你的服務已經有一個URL，使用這個URL作為issuer是一個好主意。例如，如果你的應用名稱為MyApp，並且運行在https://myapp.com，那麼你可以將issuer設置為"https://myapp.com"。
            //            Audience(aud)
            //意義：audience聲明定義了誰被允許使用該Token，即Token的預期接收者。這可以幫助接收方確認Token是否真的是發給他們的，從而避免Token被錯誤地使用。
            //填寫建議：和issuer類似，audience值也應該是能唯一標識預期接收者的字符串。如果Token是為了訪問特定的API而發行，那麼這個API的URL或者是一個標識這個API的唯一名稱都可以作為audience。例如，如果你的API是用於MyApp的用戶資料訪問，你可以將audience設置為"https://myapp.com/users"或者簡單地用"MyAppUsers"。
            //總結
            //選擇適當的issuer和audience值有助於加強JWT的安全性，並確保Token不會被未經授權的方錯誤地使用。在實際應用中，這兩個聲明應該根據你的具體需求來設置，以反映你的服務架構和安全策略。
            var issuer = "your_issuer";
            var audience = "your_audience";
            var secretKey = ConfigurationManager.AppSettings["JWTSecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            // 啟用JWT Bearer Authentication
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, // 啟用發行者驗證
                    ValidateAudience = true, // 啟用受眾驗證
                    ValidateIssuerSigningKey = true, // 驗證安全金鑰
                    ValidIssuer = issuer, // 設定預期的發行者值
                    ValidAudience = audience, // 設定預期的受眾值
                    IssuerSigningKey = key // 設定用於簽名Token的金鑰
                }
            });
        }
    }
}
