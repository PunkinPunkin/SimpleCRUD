using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SimpleCRUD
{
    public class WebApiApplication : System.Web.HttpApplication
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

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
        }
    }
}
