using log4net;
using log4net.Config;
using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SimpleCRUD
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            #region log4net

            FileInfo logConfig = new FileInfo(Server.MapPath(ConfigurationManager.AppSettings["log4net.Config"]));
            if (ConfigurationManager.AppSettings["log4net.Config.Watch"].ToUpper().Trim() == "TRUE")
                XmlConfigurator.ConfigureAndWatch(logConfig);
            else
                XmlConfigurator.Configure(logConfig);

            LogManager.GetLogger("SysLog").Info("Application Start.");

            #endregion
            
            //創建IoC容器
            AutofacConfig.Register(this.Application);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            try
            {
                string msg = exception.Message;
                if (exception.InnerException != null)
                    msg += Environment.NewLine + "InnerException: " + exception.InnerException.Message;
                LogManager.GetLogger("SysLog").Fatal(msg);
                HttpException httpException = (HttpException)exception;
                int httpCode = httpException.GetHttpCode();
                if (Request.HttpMethod.ToUpper() == "GET")
                    Response.Redirect("~/Home/ErrorPage/" + httpCode.ToString());
                else
                    Response.Redirect("~/Error/Status/" + httpCode.ToString());
            }
            catch { Response.Redirect("~/Error/NotFound"); }
            finally
            {
                Server.ClearError();
            }
        }

        protected void Application_End(object sender, EventArgs e)
        {
            LogManager.GetLogger("SysLog").Info("Application End.");
        }
    }
}
