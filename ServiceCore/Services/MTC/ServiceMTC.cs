using Autofac;
using DAL;
using DTO.DB.MTC;
using DTO.Enum;
using DTO.ReqInParm;
using DTO.ReqInParm.MTC;
using DTO.ReqResult;
using LibServer.Service;
using ServiceCore.BusinessLogic;
using Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ServiceCore.Services
{
    public class CServiceMTC : AService, IServiceMTC
    {
        public CServiceMTC(IComponentContext icoContext) : base(icoContext) { }

        /// <summary>
        /// 新增護送精神病人服務單
        /// </summary>
        public BasicResult CreateToHospitalRecord(MentalillnessToHospitalReqInParm parm)
        {
            using (TphMtcContext context = new TphMtcContext(DbName.TPH_MTC))
            {
                var req = new GenTwoReqInParm<ActionType, MentalillnessToHospitalReqInParm>() { Parm_01 = ActionType.Add, Parm_02 = parm };
                var result = BeginService<GenTwoReqInParm<ActionType, MentalillnessToHospitalReqInParm>, GenOneReqResult<int>>(req, context);
                result = GetAction<ISendHospitalRecord>().Execute(result.RetCode, req);
                result = CommonFinally(result);
                return new BasicResult(result.RetCode);
            }
        }

        /// <summary>
        /// 更新護送精神病人服務單
        /// </summary>
        public BasicResult UpdateToHospitalRecord(MentalillnessToHospitalReqInParm parm)
        {
            if (parm.Id > 0)
            {
                using (TphMtcContext context = new TphMtcContext(DbName.TPH_MTC))
                {
                    var req = new GenTwoReqInParm<ActionType, MentalillnessToHospitalReqInParm>() { Parm_01 = ActionType.Modify, Parm_02 = parm };
                    var result = BeginService<GenTwoReqInParm<ActionType, MentalillnessToHospitalReqInParm>, GenOneReqResult<int>>(req, context);
                    result = GetAction<ISendHospitalRecord>().Execute(result.RetCode, req);
                    result = CommonFinally(result);
                    return new BasicResult(result.RetCode);
                }
            }

            return new BasicResult(CommonCode.CheckError) { Message = "檢查錯誤 查無此案。" };
        }

        /// <summary>
        /// 刪除護送精神病人服務單
        /// </summary>
        public BasicResult DeleteToHospitalRecord(int id)
        {
            using (TphMtcContext context = new TphMtcContext(DbName.TPH_MTC))
            {
                var req = new GenTwoReqInParm<ActionType, MentalillnessToHospitalReqInParm>() { Parm_01 = ActionType.Delete, Parm_02 = new MentalillnessToHospitalReqInParm { Id = id } };
                var result = BeginService<GenTwoReqInParm<ActionType, MentalillnessToHospitalReqInParm>, GenOneReqResult<int>>(req, context);
                result = GetAction<ISendHospitalRecord>().Execute(result.RetCode, req);
                result = CommonFinally(result);
                return new BasicResult(result.RetCode);
            }
        }

        /// <summary>
        /// 取得警政單位
        /// </summary>
        public ResultList<PoliceStationReqInParm> GetPoliceStation(string id)
        {
            var req = new GenOneStringReqInParm() { Text = id };
            using (TphMtcContext context = new TphMtcContext(DbName.TPH_MTC))
            {
                var result = BeginService<GenOneStringReqInParm, GenOneReqResult<List<PoliceStation>>>(req, context);
                try
                {
                    var query = GetAction<IGetPoliceStationById>();
                    result = query.Execute(result.RetCode, req);
                    result = CommonFinally(result);

                    return new ResultList<PoliceStationReqInParm>()
                    {
                        Code = result.RetCode.ReturnCode,
                        Message = result.RetCode.MessageText,
                        Data = result.Result_01.Select(o => new PoliceStationReqInParm { Address = o.Address, IsDeleted = o.IsDeleted, Name = o.Name, Tel = o.Tel, Zip = o.Zip })
                    };
                }
                catch (Exception ex)
                {
                    Rollback(result, ex);
                    return new ResultList<PoliceStationReqInParm>()
                    {
                        Code = result.RetCode.ReturnCode,
                        Message = result.RetCode.MessageText,
                        Data = null
                    };
                }
            }
        }

        /// <summary>
        /// 上傳檔案至TPH_MTC_F
        /// </summary>
        /// <param name="file">檔案資訊</param>
        /// <returns>stream_id</returns>
        public Result<string> UploadFile(IFileReqInParm file)
        {
            //Parm_01 -> 存放的目錄
            var req = new GenTwoReqInParm<string, IFileReqInParm>() { Parm_01 = "API", Parm_02 = file };
            using (TphMtcFileContext context = new TphMtcFileContext(DbName.TPH_MTC_F))
            {
                var result = BeginService<GenTwoReqInParm<string, IFileReqInParm>, GenOneStringReqResult>(req, context);
                try
                {
                    result = GetAction<IUploadMtcFile>().Execute(result.RetCode, req);
                    result = CommonFinally(result);

                    return new Result<string>()
                    {
                        Code = result.RetCode.ReturnCode,
                        Message = result.RetCode.MessageText,
                        Data = result.Text
                    };
                }
                catch (Exception ex)
                {
                    Rollback(result, ex);
                    return new Result<string>()
                    {
                        Code = result.RetCode.ReturnCode,
                        Message = result.RetCode.MessageText,
                        Data = null
                    };
                }
            }
        }

        public BasicResult DeleteFile(Guid id)
        {
            var req = new GenOneReqInParm<Guid>() { Parm_01 = id };

            using (TphMtcFileContext context = new TphMtcFileContext(DbName.TPH_MTC_F))
            {
                var result = BeginService<GenOneReqInParm<Guid>, BaseReqResult>(req, context);
                try
                {
                    result = GetAction<IDeleteMtcFile>().Execute(result.RetCode, req);
                    result = CommonFinally(result);
                }
                catch (Exception ex)
                {
                    Rollback(result, ex);
                }

                return new BasicResult()
                {
                    Code = result.RetCode.ReturnCode,
                    Message = result.RetCode.MessageText
                };
            }
        }
    }
}
