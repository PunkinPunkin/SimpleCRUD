using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using SimpleCRUD.Core.Module;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SimpleCRUD
{
    public static class AutofacConfig
    {
        /// <summary>
        /// Autofac 組態註冊
        /// </summary>
        public static void Register(HttpApplicationState appliaction)
        {
            var builder = new ContainerBuilder();
            BuildMvcContainer(builder);
            BuildWebApiContainer(builder);

            //固定註冊
            builder.RegisterModule(new RegularizeModule(appliaction));

            //依需要客製註冊
            builder.RegisterModule(new CustomizeModule());

            var container = builder.Build();
            //建立相依解析器
            var resolver = new AutofacDependencyResolver(container);
            var apiResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(resolver);
            //組態Web API相依解析器
            GlobalConfiguration.Configuration.DependencyResolver = apiResolver;
        }

        #region Build MVC Autofac Container

        /// <summary>
        /// 自動增加 Autofac MVC、WebAPI Rule
        /// </summary>
        private static void BuildMvcContainer(ContainerBuilder builder)
        {
            //註冊Controller
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            //註冊Model Binder
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly()); //全部註冊
            builder.RegisterModelBinderProvider();

            //注入HTTP抽像類(MVC集成的Autofac模塊將會為HTTP抽像類添加HTTP 請求的生命收起範圍內的註冊。) 如: HttpContextBase、HttpRequestBase、HttpResponseBase...
            builder.RegisterModule(new AutofacWebTypesModule());

            //注入View page
            builder.RegisterSource(new ViewRegistrationSource());

            //對Filter Attribute進行屬性注入
            builder.RegisterFilterProvider();
        }

        #endregion Build MVC Autofac Container

        #region Build Web API Autofac Container

        private static void BuildWebApiContainer(ContainerBuilder builder)
        {
            //註冊Web API Controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
        }

        #endregion Build Web API Autofac Container
    }
}