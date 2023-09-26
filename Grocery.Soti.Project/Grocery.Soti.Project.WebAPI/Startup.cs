using Grocery.Soti.Project.WebAPI.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(Grocery.Soti.Project.WebAPI.Startup))]

namespace Grocery.Soti.Project.WebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);//Middleware
            var provider = new AuthProvider();
            OAuthAuthorizationServerOptions option = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                TokenEndpointPath = new PathString("/token"),
                Provider = provider
            };

            app.UseOAuthAuthorizationServer(option);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());//Generate Token
        }
    }
}
