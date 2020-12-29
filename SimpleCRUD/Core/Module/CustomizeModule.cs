using Autofac;
using LibServer.Repository;
using RepositoryCore.Executor;

namespace SimpleCRUD.Core.Module
{
    public class CustomizeModule : Autofac.Module
    {
        public CustomizeModule() { }

        /// <summary>
        /// 元件註冊
        /// </summary>
        /// <param name="builder">元件註物件</param>
        protected override void Load(ContainerBuilder builder)
        {
            // 單一資料表使用之 Repository 
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            builder.RegisterGeneric(typeof(QueryByStoredProcedure<,>)).As(typeof(IQueryByStoredProcedure<,>));
            builder.RegisterGeneric(typeof(QueryForModelByStoredProcedure<>)).As(typeof(IQueryForModelByStoredProcedure<>));

            //資料操作
            builder.RegisterGeneric(typeof(CRTQuery<>)).As(typeof(IRTQuery<>));
            builder.RegisterGeneric(typeof(CRTAdd<>)).As(typeof(IRTAdd<>));
            builder.RegisterGeneric(typeof(CRTModify<>)).As(typeof(IRTModify<>));
            builder.RegisterGeneric(typeof(CRTDelete<>)).As(typeof(IRTDelete<>));

            base.Load(builder);
        }
    }
}
