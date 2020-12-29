using System.Web.Http;
using System.Web.Http.Cors;

namespace SimpleCRUD
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //https://docs.microsoft.com/zh-tw/aspnet/web-api/overview/security/enabling-cross-origin-requests-in-web-api
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );
        }
    }
}
