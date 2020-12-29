using Autofac;
using DTO.DB.MTC;
using DTO.Enum;
using DTO.Enum.MTC;
using DTO.ReqInParm;
using DTO.ReqInParm.MTC;
using DTO.ReqResult;
using LibServer.Service;
using Newtonsoft.Json;
using RepositoryCore.Executor;
using ServiceCore.Services;
using Shared;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ServiceCore.BusinessLogic
{
    public class SendHospitalRecord : ABusinessLogic<GenTwoReqInParm<ActionType, MentalillnessToHospitalReqInParm>, GenOneReqResult<int>>, ISendHospitalRecord
    {
        public SendHospitalRecord(IComponentContext icoContext) : base(icoContext) { }
        public override GenOneReqResult<int> Execute(RetCode retCode, GenTwoReqInParm<ActionType, MentalillnessToHospitalReqInParm> request)
        {
            var result = new GenOneReqResult<int> { Result_01 = 0, RetCode = retCode };
            try
            {
                if (request.Parm_01 != ActionType.Delete)
                {
                    #region 檢查單位代碼

                    ICollection<string> errors = new Collection<string>();
                    for (int i = 0; i < request.Parm_02.EscortUnits.Count(); i++)
                    {
                        if (request.Parm_02.EscortUnits.ElementAt(i).TypeName == EscortUnitType.警政單位)
                        {
                            var r = _icoContext.Resolve<IServiceMTC>().GetPoliceStation(request.Parm_02.EscortUnits.ElementAt(i).Code);
                            if (r.Data == null || !r.Data.Any())
                                errors.Add(request.Parm_02.EscortUnits.ElementAt(i).Code);
                            else
                                request.Parm_02.EscortUnits.ElementAt(i).Name = r.Data.First().Name;
                        }
                    }

                    if (errors.Any())
                    {
                        SetRetCode(retCode, CommonCode.CheckError);
                        retCode.MessageText += " 醫事機構代碼或警政單位代碼: " + string.Join(",", errors) + "不存在。";
                        return result;
                    }
                    #endregion

                    #region 檢查診斷代碼

                    if (request.Parm_02.Diagnose != null && request.Parm_02.Diagnose.Any())
                    {
                        var DiagnoseList = GetAction<IRTQuery<CodeInfo>>().Execute(retCode, s => s.Type.Equals("API_Diagnose")).ToList();
                        var list1 = request.Parm_02.Diagnose.Where(i => !DiagnoseList.Select(s => s.Code).Contains(i));
                        if (list1.Any())
                        {
                            SetRetCode(retCode, CommonCode.CheckError);
                            retCode.MessageText += " 診斷代碼" + string.Join(",", list1) + "不存在。";
                            return result;
                        }
                        else if (string.IsNullOrEmpty(request.Parm_02.DiagnoseOther) && request.Parm_02.Diagnose.Where(i => DiagnoseList.Where(s => s.Content.Equals("其他")).Select(s => s.Code).Contains(i)).Any())
                        {
                            SetRetCode(retCode, CommonCode.CheckError);
                            retCode.MessageText += " 診斷選「其他」時，必須填寫說明。";
                            return result;
                        }
                    }

                    #endregion

                    #region 檢查精神疾病症狀代碼

                    if (request.Parm_02.Symptoms != null && request.Parm_02.Symptoms.Any())
                    {
                        var SymptomList = GetAction<IRTQuery<CodeInfo>>().Execute(retCode, s => s.Type.Equals("API_Symptom")).ToList();
                        var list1 = request.Parm_02.Symptoms.Where(i => !SymptomList.Select(s => s.Code).Contains(i));
                        if (list1.Any())
                        {
                            SetRetCode(retCode, CommonCode.CheckError);
                            retCode.MessageText += " 精神疾病症狀代碼" + string.Join(",", list1) + "不存在。";
                            return result;
                        }
                        else if (string.IsNullOrEmpty(request.Parm_02.SymptomOther) && request.Parm_02.Symptoms.Where(i => SymptomList.Where(s => s.Content.Equals("其他")).Select(s => s.Code).Contains(i)).Any())
                        {
                            SetRetCode(retCode, CommonCode.CheckError);
                            retCode.MessageText += " 精神疾病症狀代碼選「其他」時，必須填寫說明。";
                            return result;
                        }
                    }

                    #endregion
                }

                MentalillnessToHospitalRecord model = new MentalillnessToHospitalRecord();
                if (request.Parm_01 != ActionType.Add)
                {
                    model = GetAction<IRTQuery<MentalillnessToHospitalRecord>>().Execute(retCode, o => o.Id == request.Parm_02.Id).FirstOrDefault();
                    if (model == null)
                    {
                        SetRetCode(retCode, CommonCode.CheckError);
                        retCode.MessageText += " 查無此案。";
                        return result;
                    }
                    if (request.Parm_01 == ActionType.Delete)
                    {
                        return Delete(retCode, model);
                    }
                }
                model.AttenderName = request.Parm_02.AttenderName;
                model.AttenderPhoneNumber = request.Parm_02.AttenderPhoneNumber;
                model.CaseAddress = request.Parm_02.CaseAddress;
                model.CaseBirthday = request.Parm_02.CaseBirthday;
                model.CaseIdentityNumber = request.Parm_02.CaseId.ToUpper();
                model.CaseName = request.Parm_02.CaseName;
                model.CasePhoneNumber = request.Parm_02.CasePhoneNumber;
                model.CaseSex = GetAction<IRTQuery<SexCode>>().Execute(retCode, s => !s.IsDelete && s.DisplayName.Equals(request.Parm_02.CaseSexType.ToString())).FirstOrDefault().Code.ToString();
                model.Comment = request.Parm_02.Comment;
                model.Diagnose = (request.Parm_02.Diagnose != null) ? string.Join(",", request.Parm_02.Diagnose) : string.Empty;
                model.DiagnoseOther = request.Parm_02.DiagnoseOther;
                model.EmergencyAgencyId = request.Parm_02.EmergencyAgencyId;
                model.EscortMain = request.Parm_02.EscortMain;
                model.EscortSup = request.Parm_02.EscortSup;
                if (request.Parm_02.EscortUnits != null)
                {
                    if (request.Parm_01 == ActionType.Modify)
                        GetAction<IRTDelete<EscortUnit>>().Execute(retCode, model.EscortUnits.ToList());
                    model.EscortUnits = request.Parm_02.EscortUnits.Select(o => new EscortUnit { Code = o.Code, Name = o.Name, Type = (int)o.TypeName }).ToList();
                }
                model.Hospital = request.Parm_02.Hospital;
                model.InPerson = request.Parm_02.InPerson.Value;
                model.IsPhoneContact = request.Parm_02.IsPhoneContact;
                model.IsTrauma = request.Parm_02.IsTrauma;
                model.LocationArea = request.Parm_02.LocationArea;
                model.Location = request.Parm_02.Location;
                model.PsyHistory = (int)request.Parm_02.PsyHistoryType;
                model.Relationship = GetAction<IRTQuery<CodeInfo>>().Execute(retCode, s => s.Type.Equals("CMI_C_REAL") && s.Content.Equals(request.Parm_02.RelationshipType.ToString())).FirstOrDefault().Code;
                model.SourceName = request.Parm_02.Sources.Where(o => o.TypeName == SourceType.社政單位).Select(o => o.PersonName).FirstOrDefault();
                model.SourcePhoneNumber = request.Parm_02.Sources.Where(o => o.TypeName == SourceType.社政單位).Select(o => o.PhoneNumber).FirstOrDefault();
                model.Sources = string.Join(",", request.Parm_02.Sources.Select(o => (int)o.TypeName).Distinct());
                model.SupportContent = request.Parm_02.SupportContent;
                model.SymptomOther = request.Parm_02.SymptomOther;
                model.Symptoms = (request.Parm_02.Symptoms != null) ? string.Join(",", request.Parm_02.Symptoms) : string.Empty;
                model.WorkTime = request.Parm_02.WorkTime;

                model.IntfDate = DateTime.Now;
                model.CreateIp = request.Parm_02.CreateIp;
                model.UpdateIp = request.Parm_02.UpdateIp;
                model.UpdaterId = request.Parm_02.UpdateId;
                model.UpdateDate = DateTime.Now;

                Logger.Debug("model = " + JsonConvert.SerializeObject(model));

                #region 上傳檔案
                Result<string> uploadResult;
                if (request.Parm_02.MedicalPersonImage != null)
                {
                    uploadResult = _icoContext.Resolve<IServiceMTC>().UploadFile(request.Parm_02.MedicalPersonImage);
                    if (Guid.TryParse(uploadResult.Data, out _))
                    {
                        _icoContext.Resolve<IServiceMTC>().DeleteFile(model.MedicalPersonImage);
                        model.MedicalPersonImage = new Guid(uploadResult.Data);
                    }
                    else
                    {
                        SetRetCode(retCode, CommonCode.Fail, "上傳醫護人員簽名圖檔失敗。");
                        return result;
                    }
                }
                if (request.Parm_02.FormImage != null)
                {
                    uploadResult = _icoContext.Resolve<IServiceMTC>().UploadFile(request.Parm_02.FormImage);
                    if (Guid.TryParse(uploadResult.Data, out _))
                    {
                        if (model.FormImage.HasValue)
                            _icoContext.Resolve<IServiceMTC>().DeleteFile(model.FormImage.Value);
                        model.FormImage = new Guid(uploadResult.Data);
                    }
                    else
                    {
                        SetRetCode(retCode, CommonCode.Fail, "上傳表單圖檔失敗。");
                        return result;
                    }
                }
                #endregion

                if (request.Parm_01 == ActionType.Add)
                {
                    if (GetAction<IRTAdd<MentalillnessToHospitalRecord>>().Execute(retCode, new List<MentalillnessToHospitalRecord> { model }))
                        result.Result_01 = 1;
                    else
                        SetRetCode(retCode, CommonCode.InsertDataFail, "精神病人醫療服務單");
                }
                else if (request.Parm_01 == ActionType.Modify)
                {
                    if (GetAction<IRTModify<MentalillnessToHospitalRecord>>().Execute(retCode, new List<MentalillnessToHospitalRecord> { model }))
                        result.Result_01 = 1;
                    else
                        SetRetCode(retCode, CommonCode.UpdateDataFail, "精神病人醫療服務單");
                }

                result.RetCode = retCode;
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                SetRetCode(retCode, CommonCode.Fail, ex.Message);
            }
            return result;
        }

        protected GenOneReqResult<int> Delete(RetCode retCode, MentalillnessToHospitalRecord model)
        {
            var result = new GenOneReqResult<int> { Result_01 = 0, RetCode = retCode };

            #region 刪圖檔
            _icoContext.Resolve<IServiceMTC>().DeleteFile(model.MedicalPersonImage);
            if (model.FormImage.HasValue)
            {
                _icoContext.Resolve<IServiceMTC>().DeleteFile(model.FormImage.Value);
            }
            #endregion

            if (GetAction<IRTDelete<MentalillnessToHospitalRecord>>().Execute(retCode, new List<MentalillnessToHospitalRecord> { model }))
                SetRetCode(retCode, CommonCode.OK);
            else
                SetRetCode(retCode, CommonCode.DeleteDataFail, "id=" + model.Id.ToString());

            return result;
        }
    }

    public class GetPoliceStationById : ABusinessLogic<GenOneStringReqInParm, GenOneReqResult<List<PoliceStation>>>, IGetPoliceStationById
    {
        public GetPoliceStationById(IComponentContext icoContext) : base(icoContext) { }
        public override GenOneReqResult<List<PoliceStation>> Execute(RetCode retCode, GenOneStringReqInParm request)
        {
            var result = new GenOneReqResult<List<PoliceStation>>() { RetCode = retCode };
            try
            {
                if (int.TryParse(request.Text, out int id))
                {
                    var action = GetAction<IRTQuery<PoliceStation>>();
                    result.Result_01 = action.Execute(retCode, s => s.Id.Equals(id)).ToList();
                }
                else
                {
                    SetRetCode(retCode, CommonCode.ParameterError);
                }
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                SetRetCode(retCode, CommonCode.Fail, ex.Message);
            }
            return result;
        }
    }

    public class UploadMtcFile : ABusinessLogic<GenTwoReqInParm<string, IFileReqInParm>, GenOneStringReqResult>, IUploadMtcFile
    {
        public UploadMtcFile(IComponentContext icoContext) : base(icoContext) { }
        public override GenOneStringReqResult Execute(RetCode retCode, GenTwoReqInParm<string, IFileReqInParm> req)
        {
            var result = new GenOneStringReqResult() { RetCode = retCode };
            try
            {
                SqlParameter[] parameters = new SqlParameter[] {
                            new SqlParameter("@subPath", req.Parm_01),
                            new SqlParameter("@fileName", req.Parm_02.NameWithTimespan),
                            new SqlParameter("@fileData", req.Parm_02.Content),
                            new SqlParameter("@streamId", SqlDbType.UniqueIdentifier)
                        };
                parameters[3].Direction = ParameterDirection.Output;
                var dbResult = GetAction<IQueryByStoredProcedure<Document, string>>().Execute(retCode, new GenTwoReqInParm<string, IEnumerable<IDataParameter>>
                {
                    Parm_01 = "USP_AddDocument",
                    Parm_02 = parameters
                });

                result.Text = parameters[3].Value.ToString();
                if (string.IsNullOrEmpty(result.Text))
                    SetRetCode(retCode, CommonCode.Fail, "發生預期外的錯誤。");
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                SetRetCode(retCode, CommonCode.Fail, ex.Message);
            }
            return result;
        }
    }

    public class DeleteMtcFile : ABusinessLogic<GenOneReqInParm<Guid>, BaseReqResult>, IDeleteMtcFile
    {
        public DeleteMtcFile(IComponentContext icoContext) : base(icoContext) { }
        public override BaseReqResult Execute(RetCode retCode, GenOneReqInParm<Guid> req)
        {
            BaseReqResult result = new BaseReqResult() { RetCode = retCode };
            try
            {
                if (GetAction<IRTDelete<Document>>().Execute(retCode, GetAction<IRTQuery<Document>>().Execute(retCode, o => o.StreamId == req.Parm_01).ToList()))
                    SetRetCode(retCode, CommonCode.OK);
                else
                    SetRetCode(retCode, CommonCode.DeleteDataFail, "stream_id=" + req.Parm_01.ToString());
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                SetRetCode(retCode, CommonCode.Fail, ex.Message);
            }
            return result;
        }
    }
}
