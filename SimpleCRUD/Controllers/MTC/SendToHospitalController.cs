using Autofac;
using DTO.Enum;
using DTO.ReqInParm.MTC;
using DTO.ReqResult;
using log4net;
using ServiceCore.Services;
using Shared;
using SimpleCRUD.Core.Controller;
using System;
using System.Web.Http;

namespace DataExchangeApi.Controllers.Api.MTC
{
    [RoutePrefix("api/MTC")]
    public class SendToHospitalController : DExApiController
    {
        public SendToHospitalController(IComponentContext iocContext, ILog logger) : base(iocContext, logger, Platform.MTC) { }

        [Route("SendHospitalRecord")]
        public IHttpActionResult Post(MentalillnessToHospitalReqInParm parm)
        {
            BasicResult result = null;
            try
            {
                CheckModel(ref result);
                if (result.Code == CommonCode.OK.ToResCode())
                {
                    parm.CreateIp = ClientIp;
                    parm.UpdateIp = ClientIp;
                    result = _iocContext.Resolve<IServiceMTC>().CreateToHospitalRecord(parm);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Fatal(ControllerName + "-新增精神病人醫療服務單：" + ex.Message);
                return Redirect(ErrorUrl);
            }
            finally
            {
                //TODO: write api log to db
            }
        }

        [Route("SendHospitalRecord")]
        public IHttpActionResult Put(MentalillnessToHospitalReqInParm parm)
        {
            BasicResult result = null;
            try
            {
                CheckModel(ref result);
                if (result.Code == CommonCode.OK.ToResCode())
                {
                    parm.CreateIp = ClientIp;
                    parm.UpdateIp = ClientIp;
                    result = _iocContext.Resolve<IServiceMTC>().UpdateToHospitalRecord(parm);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Fatal(ControllerName + "-修改精神病人醫療服務單：" + ex.Message);
                return Redirect(ErrorUrl);
            }
            finally
            {
                //TODO: write api log to db
            }
        }

        [Route("SendHospitalRecord/{id}")]
        public IHttpActionResult Delete(int id)
        {
            BasicResult result = null;
            try
            {
                CheckModel(ref result);
                if (result.Code == CommonCode.OK.ToResCode())
                {
                    result = _iocContext.Resolve<IServiceMTC>().DeleteToHospitalRecord(id);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Fatal(ControllerName + "-刪除精神病人醫療服務單：" + ex.Message);
                return Redirect(ErrorUrl);
            }
            finally
            {
                //TODO: write api log to db
            }
        }
    }
}