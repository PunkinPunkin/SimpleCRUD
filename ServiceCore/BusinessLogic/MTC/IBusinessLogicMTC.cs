using DTO.DB.MTC;
using DTO.Enum;
using DTO.ReqInParm;
using DTO.ReqInParm.MTC;
using DTO.ReqResult;
using LibServer.Service;
using System;
using System.Collections.Generic;

namespace ServiceCore.BusinessLogic
{
    public interface ISendHospitalRecord: IBusinessLogic<GenTwoReqInParm<ActionType, MentalillnessToHospitalReqInParm>, GenOneReqResult<int>> { }
    public interface IGetSexCodeByDesc : IBusinessLogic<GenOneReqInParm<SexType>, GenOneReqResult<List<SexCode>>> { }
    public interface IGetPoliceStationById : IBusinessLogic<GenOneStringReqInParm, GenOneReqResult<List<PoliceStation>>> { }

    /// <summary>
    /// 上傳檔案至TPH_MTC_F, CGenTwoReqInParm: Parm_01 -> 存放的目錄, Parm_02 -> 檔案資訊
    /// </summary>
    public interface IUploadMtcFile : IBusinessLogic<GenTwoReqInParm<string, IFileReqInParm>, GenOneStringReqResult> { }
    public interface IDeleteMtcFile : IBusinessLogic<GenOneReqInParm<Guid>, Shared.BaseReqResult> { }
}
