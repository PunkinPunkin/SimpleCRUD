using Autofac;
using LibServer.DataBase;
using log4net;
using Newtonsoft.Json;
using Shared;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;

namespace LibServer.Service
{
    public abstract class AService : IService
    {
        /// <summary>
        /// Autofac Context 物件
        /// </summary>
        protected readonly IComponentContext _icoContext;
        private static ResourceManager _resManager = null;
        private IUnitOfWork _unit;

        /// <summary>
        /// 建構元
        /// </summary>
        /// <param name="icoContext">Autofac Context 物件(由Autoface自動生成)</param>
        public AService(IComponentContext icoContext)
        {
            _icoContext = icoContext;
        }

        /// <summary>
        /// 撰寫 Log 物件
        /// </summary>
        public ILog Logger { get; set; }

        /// <summary>
        /// 環境變數(Develop、Test、Production), 由 Web.Config appSettings給定
        /// </summary>
        public string Environment { set; get; }

        /// <summary>
        /// 服務方法的編號
        /// </summary>
        public int? Seq { get; set; }

        /// <summary>
        /// 之前服務方法的編號
        /// </summary>
        public int? PreSeq { get; set; }

        /// <summary>
        /// DataBase 連線管理物件
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 取得目前方法名稱
        /// </summary>
        /// <returns>方法名稱</returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);
            return sf.GetMethod().Name;
        }

        /// <summary>
        /// 取得預設回傳訊息
        /// </summary>
        /// <returns>回傳訊息</returns>
        protected RetCode DefaultRetCode()
        {
            return new RetCode()
            {
                ReturnCode = CommonCode.OK.ToResCode(),
                MessageText = CommonCode.OK.ToStringValue(),
                Environment = this.Environment
            };
        }

        /// <summary>
        /// 服務方法初時化
        /// </summary>
        /// <typeparam name="R">請求物件型態</typeparam>
        /// <typeparam name="T">回傳物件型態</typeparam>
        /// <param name="req">請求物件</param>
        /// <param name="dbContext">連接資料庫</param>
        /// <returns>回傳物件</returns>
        protected T BeginService<R, T>(R req, DbContext dbContext)
            where R : class, new()
            where T : BaseReqResult, new()
        {
            Logger.Info(JsonConvert.SerializeObject(req));
            T result = new T()
            {
                RetCode = DefaultRetCode()
            };

            _unit = new UnitOfWork(dbContext, this.Environment)
            {
                Logger = LogManager.GetLogger("SysLog")
            };
            return result;
        }

        /// <summary>
        /// 服務完成方法
        /// </summary>
        /// <typeparam name="T">回傳物件型態</typeparam>
        /// <param name="result">回傳物件</param>
        /// <returns>回傳物件</returns>
        /// <remarks>
        ///     本方法在回傳Code為OK就會呼叫<see cref="Commit()"/>否則就會呼叫<see cref="Rollback()"/>
        /// </remarks>
        protected T CommonFinally<T>(T result) where T : BaseReqResult
        {
            try
            {
                if (result.RetCode.IsOK)
                    Commit();
                else
                    Rollback();
                //只留本區域的MsgCode
                if (result.RetCode.MsgSequence != null)
                {
                    result.RetCode.MsgSequence
                          .Where(r => r.Type == MsgType.MSGCODE)
                          .ToList().ForEach(r => result.RetCode.MsgSequence.Remove(r));
                }

                if (result.RetCode.ReturnCode == CommonCode.OK.ToResCode() ||
                    result.RetCode.ReturnCode == CommonCode.AlreadyHaveToken.ToResCode() ||
                    result.RetCode.ReturnCode == CommonCode.PwdStrength.ToResCode() ||
                    result.RetCode.ReturnCode == CommonCode.TokenExpired.ToResCode())
                    Logger.Info(JsonConvert.SerializeObject(result.RetCode));
                else
                    Logger.Fatal(JsonConvert.SerializeObject(result.RetCode));
            }
            catch (DbEntityValidationException ex)
            {
                var getFullMessage = ExceptionHelper.GetValidationErrors(ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors));
                var exceptionMessage = string.Concat(ex.Message, getFullMessage);

                Logger.Fatal(exceptionMessage);
                SetRetCode(result.RetCode, CommonCode.Fail, getFullMessage);
            }
            catch (DbUpdateException ex)
            {
                Exception exception = ExceptionHelper.HandleDbUpdateException(ex);
                List<string> errList = new List<string> { ex.Message };
                ExceptionHelper.GetAllInnerExceptionMessage(ex, ref errList);
                Logger.Fatal(string.Join(System.Environment.NewLine, errList));
                SetRetCode(result.RetCode, CommonCode.Fail, "更新失敗");
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex);
                SetRetCode(result.RetCode, CommonCode.Fail, "發生預期外錯誤");
            }

            return result;
        }

        /// <summary>
        /// 取得<see cref="IBusinessLogic{R, T}"/>實作物件
        /// </summary>
        /// <typeparam name="T">實作Business Logic介面</typeparam>
        /// <returns>Business Logic實作物件</returns>
        protected T GetAction<T>()
        {
            var bl = (IComponent)_icoContext.Resolve(typeof(T));
            bl.UnitOfWork = _unit;
            if (bl.GetType().BaseType.Name == typeof(ABusinessLogic<,>).Name)
            {
                return (T)bl;
            }
            return default;
        }

        /// <summary>
        /// 資料庫連線 Commit
        /// </summary>
        protected void Commit()
        {
            if (_unit != null)
                _unit.Commit();
        }

        /// <summary>
        /// 資料庫連線 Rollback
        /// </summary>
        protected void Rollback()
        {
            if (_unit != null)
                _unit.Rollback();
        }

        protected void Rollback(BaseReqResult result, Exception ex)
        {
            SetRetCode(result.RetCode, CommonCode.Fail, ex.Message);
            Logger.Fatal(JsonConvert.SerializeObject(result), ex);
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
                retCode.MessageText = string.Format(returnCode.ToStringValue(), parms);
            else
                retCode.MessageText = returnCode.ToStringValue();
        }

        /// <summary>
        ///  提供用於釋放 Unmanaged 資源的機制
        /// </summary>
        public void Dispose()
        {
            if (_unit != null)
                _unit.Dispose();
            GC.Collect();
        }
    }
}
