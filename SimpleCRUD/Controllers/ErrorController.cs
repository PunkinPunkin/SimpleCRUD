using Autofac;
using DTO.ReqResult;
using log4net;
using Shared;
using SimpleCRUD.Core.Controller;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SimpleCRUD.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/Error")]
    public class ErrorController : CustApiController
    {
        public ErrorController(IComponentContext iocContext, ILog logger) : base(iocContext, logger) { }

        [Route("Status/{code:int}")]
        [HttpGet, HttpPost, HttpPut, HttpDelete]
        public IHttpActionResult Status(int? code)
        {
            if (code.HasValue)
            {
                if (Enum.TryParse(code.Value.ToString(), out HttpStatusCode StatusCode))
                {
                    return ResponseMessage(Request.CreateResponse(StatusCode));
                }
            }
            return NotFound();
        }

        [Route("NotFound")]
        [HttpGet, HttpPost, HttpPut, HttpDelete]
        public IHttpActionResult PageNotFound()
        {
            return NotFound();
        }

        [Route("Token")]
        [HttpGet, HttpPost, HttpPut, HttpDelete]
        [ResponseType(typeof(BasicResult))]
        public IHttpActionResult Token()
        {
            return ResponseMessage(
                Request.CreateResponse(
                    HttpStatusCode.Forbidden,
                    new BasicResult() { Code = CommonCode.InvalidToken.ToResCode(), Message = "Invalid Token" }
                    )
                );
        }
    }
}
