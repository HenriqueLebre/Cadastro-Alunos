using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using Microsoft.Owin.Cors;
using Swashbuckle.Application;
using Microsoft.Owin.Security.OAuth;

[assembly: OwinStartup(typeof(WebApp3.Startup))]

namespace WebApp3 {
    public class Startup {
        public void Configuration(IAppBuilder app) {
            var config = new HttpConfiguration();


            config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.EnableSwagger(c => {
                c.SingleApiVersion("v1", "WebApp3");
                c.IncludeXmlComments(AppDomain.CurrentDomain.BaseDirectory + @"\bin\WebApp3.xml");

            });


            app.UseCors(CorsOptions.AllowAll);

            AtivandoAcessTokens(app);

            app.UseWebApi(config);
        }

        private void AtivandoAcessTokens(IAppBuilder app) {
            var opcoesConfiguracaoToken = new OAuthAuthorizationServerOptions() {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                Provider = new ProviderDeTokensDeAcesso()
            };

            app.UseOAuthAuthorizationServer(opcoesConfiguracaoToken);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
