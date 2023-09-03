//using Microsoft.Owin;
//using Microsoft.Owin.Security.Google;
//using Owin;
//using Microsoft.Owin.Security.Cookies;

//[assembly: OwinStartup(typeof(HealthHub2.Startup))]

//namespace HealthHub2 // 确保这是您实际项目中的命名空间
//{
//    public class Startup
//    {
//        public void Configuration(IAppBuilder app)
//        {
//            // Enable the application to use a cookie to store information for the signed in user
//            app.UseCookieAuthentication(new CookieAuthenticationOptions
//            {
//                AuthenticationType = "ApplicationCookie",
//                LoginPath = new PathString("/Account/Login"), // 更改为您的登录页面路径
//            });

//            // Enable Google authentication
//            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
//            {
//                ClientId = "787950472909-m6nrm890u342g8s3mnd23dffbaes04ge.apps.googleusercontent.com",
//                ClientSecret = "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",
//                // 这里的回调路径应与 Google Cloud Console 中设置的路径一致。
//                CallbackPath = new PathString("/signin-google")
//            }) ;
//        }
//    }
//}



