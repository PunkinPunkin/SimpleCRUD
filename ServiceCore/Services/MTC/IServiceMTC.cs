using DTO.ReqInParm;
using DTO.ReqInParm.MTC;
using DTO.ReqResult;
using System;

namespace ServiceCore.Services
{
    public interface IServiceMTC
    {
        /// <summary>
        /// 新增護送精神病人服務單
        /// </summary>
        BasicResult CreateToHospitalRecord(MentalillnessToHospitalReqInParm parm);

        /// <summary>
        /// 更新護送精神病人服務單
        /// </summary>
        BasicResult UpdateToHospitalRecord(MentalillnessToHospitalReqInParm parm);

        /// <summary>
        /// 刪除護送精神病人服務單
        /// </summary>
        BasicResult DeleteToHospitalRecord(int id);

        /// <summary>
        /// 取得警政單位
        /// </summary>
        ResultList<PoliceStationReqInParm> GetPoliceStation(string id);

        /// <summary>
        /// 儲存檔案至File Table
        /// </summary>
        /// <param name="file">檔案資訊</param>
        /// <returns>stream_id</returns>
        Result<string> UploadFile(IFileReqInParm file);

        BasicResult DeleteFile(Guid id);
    }
}
