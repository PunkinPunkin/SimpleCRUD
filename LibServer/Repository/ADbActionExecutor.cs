using LibServer.DataBase;
using log4net;
using Shared;
using Shared.DTO;
using System;
using System.Linq;

namespace LibServer.Repository
{
    /// <summary>
    /// Repository 抽象物件
    /// </summary>
    /// <typeparam name="R">Return 物件</typeparam>
    /// <typeparam name="T">參數物件</typeparam>
    public abstract class ADbActionExecutor<R, T>
    {
        private bool _disposed = false;

        /// <summary>
        /// Database 管理物件
        /// </summary>
        protected IUnitOfWork _unit;

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
        /// 執行與釋放 (Free)、釋放 (Release) 或重設 Unmanaged 資源相關聯之應用程式定義的工作。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void SetRetCode(RetCode retCode, CommonCode commonCode, params string[] p)
        {
            retCode.ReturnCode = commonCode.ToResCode();
            if (p != null && p.Any())
                retCode.MessageText = string.Format(commonCode.ToStringValue(), p);
            else
                retCode.MessageText = commonCode.ToStringValue();
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
        ~ADbActionExecutor()
        {
            Dispose(false);
        }
    }
}
