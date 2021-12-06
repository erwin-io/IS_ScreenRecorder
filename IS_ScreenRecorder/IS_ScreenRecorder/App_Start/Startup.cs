using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using Microsoft.Owin.Cors;
using Owin;

namespace IS_ScreenRecorder.App_Start
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { action = "get", id = RouteParameter.Optional }
            );
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
            app.UseWebApi(config);


        }
    }
}
