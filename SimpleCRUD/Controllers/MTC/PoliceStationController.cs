using Autofac;
using DTO.Enum;
using DTO.ReqInParm.MTC;
using DTO.ReqResult;
using log4net;
using ServiceCore.Services;
using SimpleCRUD.Core.Controller;
using System;
using System.Web.Http;
using System.Web.Http.Description;

namespace DataExchangeApi.Controllers.Api.MTC
{
    [RoutePrefix("api/MTC/PoliceStation")]
    public class PoliceStationController : DExApiController
    {
        public PoliceStationController(IComponentContext iocContext, ILog logger) : base(iocContext, logger, Platform.MTC) { }

        [ResponseType(typeof(ResultList<PoliceStationReqInParm>))]
        public IHttpActionResult Get([FromUri]string id)
        {
            try
            {
                var result = _iocContext.Resolve<IServiceMTC>().GetPoliceStation(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Fatal(ControllerName + "-查詢警政單位：" + ex.Message);
                return Redirect(ErrorUrl);
            }
            finally
            {
                //TODO: write api log to db
            }
        }
    }
}