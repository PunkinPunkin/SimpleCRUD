using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using LibServer;
using LibServer.Repository;
using LibServer.Service;
using log4net;
using Shared;
using Shared.Helper;
using System;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SimpleCRUD.Core.Module
{
    public class RegularizeModule : Autofac.Module
    {
        private HttpApplicationState _appliaction;
        public static EnvType _envTyp = (EnvType)Enum.Parse(typeof(EnvType), ConfigurationManager.AppSettings["Environment"]);
        public RegularizeModule(HttpApplicationState appliaction)
        {
            _appliaction = appliaction;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var helper = new AssemblyHelper();

            //註冊 ServiceCore.dll 中在 ServiceCore.Services 命名空間下並且繼承 IService 的 ALL 類別(Service註冊)
            helper.GetAllAssembly("ServiceCore.dll")
                  .ForEach(a => builder.RegisterAssemblyTypes(a)
                                       .Where(t => t.Namespace != null &&
                                                   t.Namespace.StartsWith("ServiceCore.Services") &&
                                                   t.GetInterfaces().Any(i => i == typeof(IService)))
                                       .AsImplementedInterfaces());

            //註冊 ServiceCore.dll 中在 ServiceCore.BusinessLogic 命名空間下並且繼承 IBusinessLogic<,> 的 ALL 類別(BusinessLogic註冊)
            helper.GetAllAssembly("ServiceCore.dll")
                  .ForEach(a => builder.RegisterAssemblyTypes(a)
                                       .Where(t => t.Namespace != null &&
                                                   t.Namespace.StartsWith("ServiceCore.BusinessLogic") &&
                                                   t.GetInterfaces().Any(i => i.Name == typeof(IBusinessLogic<,>).Name))
                                       .AsImplementedInterfaces());

            //註冊 RepositoryCore.dll 中在 RepositoryCore.Executor 命名空間下並且繼承 IRepositoryAction<,> 的 ALL 類別(Repository註冊)
            helper.GetAllAssembly("RepositoryCore.dll")
                  .ForEach(a => builder.RegisterAssemblyTypes(a)
                                       .Where(t => t.Namespace != null &&
                                                   t.Namespace.StartsWith("RepositoryCore.Executor") &&
                                                   t.GetInterfaces().Any(i => i.Name == typeof(IRepositoryAction<,>).Name))
                                       .AsImplementedInterfaces());

            base.Load(builder);
        }

        protected override void AttachToComponentRegistration(IComponentRegistryBuilder componentRegistry, IComponentRegistration registration)
        {
            registration.Preparing += registration_Preparing;
            registration.Activated += registration_Activated;
        }

        private void registration_Activated(object sender, ActivatedEventArgs<object> e)
        {
            if (e.Instance is AService)
            {
                var service = (AService)e.Instance;
                service.Logger = LogManager.GetLogger("SysLog");
                service.Environment = System.Web.Configuration.WebConfigurationManager.AppSettings["Environment"];
                var seq = HttpContext.Current.Response.Headers["SEQ"];
                service.Seq = string.IsNullOrWhiteSpace(seq) ? new int?() : int.Parse(seq);
                var preSeq = HttpContext.Current.Response.Headers["PRE_SEQ"];
                service.PreSeq = string.IsNullOrWhiteSpace(preSeq) ? new int?() : int.Parse(preSeq);
            }
            else if (e.Instance is IComponent)
            {
                var component = (IComponent)e.Instance;
                component.Logger = LogManager.GetLogger("SysLog");
                component.Environment = System.Web.Configuration.WebConfigurationManager.AppSettings["Environment"];
            }
        }

        private void registration_Preparing(object sender, Autofac.Core.PreparingEventArgs e)
        {
            var t = e.Component.Activator.LimitType;
            e.Parameters = e.Parameters.Union(
                new[]
                {
                    new ResolvedParameter(
                        (p, i) => p.ParameterType == typeof(ILog), (p, i) => LogManager.GetLogger(t) //註冊 Log4net
                    )
                }
            );
        }
    }
}