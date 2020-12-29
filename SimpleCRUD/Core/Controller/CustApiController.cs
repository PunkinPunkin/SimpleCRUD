using Autofac;
using log4net;
using Shared;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace SimpleCRUD.Core.Controller
{
    public class CustApiController : System.Web.Http.ApiController, ICustController
    {
        protected readonly IComponentContext _iocContext;
        /// <summary>
        /// 要先在log4net.config設定logger名稱
        /// </summary>
        protected readonly ILog _logger;

        #region 屬性

        public EnvType Env => (EnvType)Enum.Parse(typeof(EnvType), ConfigurationManager.AppSettings["Environment"]);

        public string ClientIp
        {
            get
            {
                if (Request.Properties.ContainsKey("MS_HttpContext"))
                {
                    return ((HttpContextWrapper)Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
                }
                else if (HttpContext.Current != null)
                {
                    return HttpContext.Current.Request.UserHostAddress;
                }
                else
                {
                    return null;
                }
            }
        }

        public string ActionName
        {
            get { return this.ControllerContext.RouteData.Values["action"].ToString(); }
        }

        public string ControllerName
        {
            get { return this.ControllerContext.RouteData.Values["controller"].ToString(); }
        }

        /// <summary>
        /// 基本錯誤url，可導到404，後面可加其他Http Status Code。
        /// </summary>
        public string ErrorUrl => Url.Content("~/Error/Status/");

        /// <summary>
        /// Toke驗證失敗url
        /// </summary>
        public string TokenErrorUrl => Url.Content("~/Error/Token");

        /// <summary>
        /// 存放Token的欄位名
        /// </summary>
        public readonly string TokenName = "Token";


        protected ResponseMessageResult BadRequest<T>(T value)
        {
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, value));
        }

        protected IHttpActionResult CommonFinally<T>(T result, HttpStatusCode? statusCode = null) where T : DTO.ReqResult.IBasicResult
        {
            if (statusCode == null)
            {
                if (result.Code == CommonCode.OK.ToResCode())
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            else
                return ResponseMessage(Request.CreateResponse(statusCode.Value, result));
        }
        #endregion 屬性

        public CustApiController(IComponentContext iocContext, ILog logger)
        {
            _iocContext = iocContext;
            _logger = logger;
        }
    }
}