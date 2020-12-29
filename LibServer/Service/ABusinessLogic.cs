using Autofac;
using LibServer.DataBase;
using LibServer.Repository;
using log4net;
using Shared;
using Shared.DTO;
using System;
using System.Linq;

namespace LibServer.Service
{
    /// <summary>
    /// BusinessLogic 抽象物件
    /// </summary>
    /// <typeparam name="R">Return 物件</typeparam>
    /// <typeparam name="T">參數物件</typeparam>
    public abstract class ABusinessLogic<R, T>
    {
        /// <summary>
        /// Autofac Context 物件(由Autoface自動生成)
        /// </summary>
        protected readonly IComponentContext _icoContext;
        private bool _disposed = false;
        private IUnitOfWork _unit;

        /// <summary>
        /// 建構元
        /// </summary>
        /// <param name="icoContext">Autofac Context 物件(由Autoface自動生成)</param>
        protected ABusinessLogic(IComponentContext icoContext)
        {
            _icoContext = icoContext;
        }

        /// <summary>
        /// 撰寫 Log 物件
        /// </summary>
        public ILog Logger { get; set; }

        /// <summary>
        /// 環境變數
        /// </summary>
        public string Environment { set; get; }

        /// <summary>
        /// Database 管理物件
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            set { _unit = value; }
        }

        /// <summary>
        /// 執行 Action 
        /// </summary>
        /// <param name="retCode">RetCode 物件</param>
        /// <param name="request">請求參數</param>
        /// <returns></returns>
        public abstract T Execute(RetCode retCode, R request);

        /// <summary>
        /// 取得<see cref="IBusinessLogic{R, T}"/>或<see cref="IRepositoryAction{R, T}"/>實作物件
        /// </summary>
        /// <typeparam name="RT">實作Business Logic或Repository介面</typeparam>
        /// <returns>Business Logic或Repository實作物件</returns>
        protected RT GetAction<RT>()
        {
            var bl = (IComponent)_icoContext.Resolve(typeof(RT));
            bl.UnitOfWork = _unit;
            if (bl.GetType().BaseType.Name == typeof(ADbActionExecutor<,>).Name ||
                bl.GetType().BaseType.Name == typeof(ABusinessLogic<,>).Name)
            {
                return (RT)bl;
            }
            return default(RT);
        }

        /// <summary>
        /// 設定 ReturnCode 和 MessageText 方法
        /// </summary>
        /// <param name="retCode">RetCode 物件</param>
        /// <param name="returnCode">ReturnCode</param>
        /// <param name="parms">Format 參數</param>
        protected void SetRetCode(RetCode retCode, CommonCode returnCode, params string[] parms)
        {
            retCode.ReturnCode = returnCode.ToResCode();
            if (parms != null && parms.Any())
            {
                retCode.MessageText = string.Format(returnCode.ToStringValue(), parms);
            }
            else
            {
                retCode.MessageText = returnCode.ToStringValue();
            }
        }

        /// <summary>
        /// 執行與釋放 (Free)、釋放 (Release) 或重設 Unmanaged 資源相關聯之應用程式定義的工作。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 執行與釋放 (Free)、釋放 (Release) 或重設 Unmanaged 資源相關聯之應用程式定義的工作。
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            _disposed = true;
        }

        /// <summary>
        /// 解構元
        /// </summary>
        ~ABusinessLogic()
        {
            Dispose(false);
        }
    }
}
