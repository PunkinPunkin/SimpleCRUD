using Autofac;
using DTO.Enum;
using DTO.ReqResult;
using log4net;
using Shared;
using System;
using System.Linq;

namespace SimpleCRUD.Core.Controller
{
    public class DExApiController : CustApiController
    {
        protected readonly Platform _platform;
        public DExApiController(IComponentContext iocContext, ILog logger, Platform platform) : base(iocContext, logger)
        {
            _platform = platform;
        }

        #region 方法

        protected void CheckModel<T>(ref T result) where T : IBasicResult, new()
        {
            if (result == null)
                result = new T();
            if (ModelState.IsValid)
            {
                result.Code = CommonCode.OK.ToResCode();
                result.Message = CommonCode.OK.ToStringValue();
            }
            else
            {
                result.Code = CommonCode.ParameterError.ToResCode();
                result.Message = CommonCode.ParameterError.ToStringValue() + Environment.NewLine + string.Join(Environment.NewLine, ModelState.Select(m => string.Join(Environment.NewLine, m.Value.Errors.Select(e => e.ErrorMessage))));
            }
        }

        #endregion 方法
    }
}