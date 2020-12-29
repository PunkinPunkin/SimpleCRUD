using DTO.Enum;
using DTO.Enum.MTC;
using Newtonsoft.Json;
using Shared.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DTO.ReqInParm.MTC
{
    public class MentalillnessToHospitalReqInParm : IValidatableObject
    {
        #region prop

        public int Id { get; set; } = 0;

        /// <summary>
        /// 出勤時間
        /// </summary>
        [Required]
        [DisplayName("出勤時間")]
        public DateTime WorkTime { get; set; }

        /// <summary>
        /// 通報來源
        /// </summary>
        [Required]
        [DisplayName("通報來源")]
        public ICollection<CNoticeSourceReqInParm> Sources { get; set; }

        /// <summary>
        /// 個案姓名
        /// </summary>
        [Required]
        [DisplayName("個案姓名")]
        [MaxLength(50, ErrorMessage = "{0} 最大長度為{1}。")]
        public string CaseName { get; set; }

        /// <summary>
        /// 個案身分證/居留證
        /// </summary>
        [Required]
        [DisplayName("個案身分證/居留證")]
        [MaxLength(20, ErrorMessage = "{0} 最大長度為{1}。")]
        [TaiwanIdentityCardNumber(ErrorMessage = "身分證字號格式不符。")]
        public string CaseId { get; set; }

        /// <summary>
        /// 個案性別(男/女)
        /// </summary>
        [Required]
        [DisplayName("個案性別")]
        [MaxLength(1, ErrorMessage = "{0} 最大長度為{1}。")]
        public string CaseSex { get; set; }

        /// <summary>
        /// 個案性別
        /// </summary>
        [JsonIgnore]
        [DisplayName("個案性別")]
        [Required(ErrorMessage = "{0} 有誤。")]
        public SexType? CaseSexType
        {
            get
            {
                if (!int.TryParse(CaseSex, out int _)) //只允許傳文字
                {
                    if (System.Enum.TryParse(CaseSex, out SexType eValue))
                    {
                        if (System.Enum.IsDefined(typeof(SexType), eValue) | eValue.ToString().Contains(","))
                            return eValue;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// 個案生日
        /// </summary>
        [DisplayName("個案生日")]
        public DateTime? CaseBirthday { get; set; }

        /// <summary>
        /// 個案聯絡電話
        /// </summary>
        [Required]
        [DisplayName("個案聯絡電話")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(15, ErrorMessage = "{0} 最大長度為{1}。")]
        [RegularExpression(@"^[0-9]{1,15}", ErrorMessage = "{0} 格式錯誤(必須為純數字)。")]
        public string CasePhoneNumber { get; set; }

        /// <summary>
        /// 個案聯絡地址
        /// </summary>
        [Required]
        [DisplayName("個案聯絡地址")]
        [MaxLength(200, ErrorMessage = "{0} 最大長度為{1}。")]
        public string CaseAddress { get; set; }

        /// <summary>
        /// 事發地點-行政區
        /// </summary>
        [Required]
        [DisplayName("事發地點-行政區")]
        [MaxLength(50, ErrorMessage = "{0} 最大長度為{1}。")]
        public string LocationArea { get; set; }

        /// <summary>
        /// 事發地點
        /// </summary>
        [Required]
        [DisplayName("事發地點")]
        [MaxLength(200, ErrorMessage = "{0} 最大長度為{1}。")]
        public string Location { get; set; }

        /// <summary>
        /// 出席家屬姓名
        /// </summary>
        [Required]
        [DisplayName("出席家屬姓名")]
        [MaxLength(50, ErrorMessage = "{0} 最大長度為{1}。")]
        public string AttenderName { get; set; }

        /// <summary>
        /// 與個案關係(代碼CMI_C_REAL)
        /// </summary>
        [Required]
        [DisplayName("與個案關係")]
        public string Relationship { get; set; }

        /// <summary>
        /// 與個案關係(代碼CMI_C_REAL)
        /// </summary>
        [JsonIgnore]
        [DisplayName("與個案關係")]
        [Required(ErrorMessage = "{0} 代碼有誤。")]
        public RelationType? RelationshipType
        {
            get
            {
                if (Id > 0 && Relationship == "08")
                    return RelationType.其他; //存到DB後「其他」代碼變成08
                else if (System.Enum.TryParse(Relationship, out RelationType eValue))
                {
                    if (System.Enum.IsDefined(typeof(RelationType), eValue) | eValue.ToString().Contains(","))
                        return eValue;
                }
                return null;
            }
        }

        /// <summary>
        /// 出席者聯絡電話
        /// </summary>
        [Required]
        [DisplayName("出席者聯絡電話")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(15, ErrorMessage = "{0} 最大長度為{1}。")]
        [RegularExpression(@"^[0-9]{1,15}", ErrorMessage = "{0} 格式錯誤(必須為純數字)。")]
        public string AttenderPhoneNumber { get; set; }

        /// <summary>
        /// 護送單位
        /// </summary>
        [Required]
        [DisplayName("護送單位")]
        public ICollection<CEscortUnitReqInParm> EscortUnits { get; set; }

        /// <summary>
        /// 主護送人
        /// </summary>
        [Required]
        [DisplayName("主護送人")]
        [MaxLength(100, ErrorMessage = "{0} 最大長度為{1}。")]
        public string EscortMain { get; set; }

        /// <summary>
        /// 協助人
        /// </summary>
        [DisplayName("協助人")]
        [MaxLength(50, ErrorMessage = "{0} 最大長度為{1}。")]
        public string EscortSup { get; set; }

        /// <summary>
        /// 協助內容
        /// </summary>
        [DisplayName("協助內容")]
        [MaxLength(20, ErrorMessage = "{0} 最大長度為{1}。")]
        public string SupportContent { get; set; }

        /// <summary>
        /// 是否親自護送(true: 親自護送就醫, false: 協助送醫)
        /// </summary>
        [Required]
        [DisplayName("是否親自護送")]
        public bool? InPerson { get; set; }

        /// <summary>
        /// 是否為電洽(true: 電洽, false: 到場)
        /// </summary>
        [DisplayName("是否為電洽")]
        public bool? IsPhoneContact { get; set; }

        /// <summary>
        /// 是否為創傷(true: 創傷, false: 非創傷)
        /// </summary>
        [DisplayName("是否為創傷")]
        public bool? IsTrauma { get; set; }

        /// <summary>
        /// 後送醫療院區(醫事機構代碼)
        /// </summary>
        [DisplayName("後送醫療院區(醫事機構代碼)")]
        [MaxLength(20, ErrorMessage = "{0} 最大長度為{1}。")]
        public string EmergencyAgencyId { get; set; }

        /// <summary>
        /// 過往精神疾病史(1: 有, 2: 無, 3: 不詳)
        /// </summary>
        [Required]
        [DisplayName("過往精神疾病史")]
        public string PsyHistory { get; set; }

        /// <summary>
        /// 過往精神疾病史
        /// </summary>
        [JsonIgnore]
        [DisplayName("過往精神疾病史")]
        [Required(ErrorMessage = "{0} 代碼有誤。")]
        public PsyHistoryType? PsyHistoryType
        {
            get
            {
                if (System.Enum.TryParse(PsyHistory, out PsyHistoryType eValue))
                {
                    if (System.Enum.IsDefined(typeof(PsyHistoryType), eValue) | eValue.ToString().Contains(","))
                        return eValue;
                }
                return null;
            }
        }

        /// <summary>
        /// 診斷
        /// </summary>
        [DisplayName("診斷")]
        public ICollection<string> Diagnose { get; set; }

        /// <summary>
        /// 診斷
        /// </summary>
        [DisplayName("診斷-其他")]
        [MaxLength(30, ErrorMessage = "{0} 最大長度為{1}。")]
        public string DiagnoseOther { get; set; }

        /// <summary>
        /// 精神疾病症狀
        /// </summary>
        [DisplayName("精神疾病症狀")]
        public ICollection<string> Symptoms { get; set; }

        /// <summary>
        /// 精神疾病症狀-其他
        /// </summary>
        [DisplayName("精神疾病症狀-其他")]
        [MaxLength(50, ErrorMessage = "{0} 最大長度為{1}。")]
        public string SymptomOther { get; set; }

        /// <summary>
        /// 就醫醫院
        /// </summary>
        [Required]
        [DisplayName("就醫醫院")]
        [MaxLength(50, ErrorMessage = "{0} 最大長度為{1}。")]
        public string Hospital { get; set; }

        /// <summary>
        /// 醫護人員簽名圖檔
        /// </summary>
        [DisplayName("醫護人員簽名圖檔")]
        public ImageReqInParm MedicalPersonImage { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        [DisplayName("備註")]
        [MaxLength(500, ErrorMessage = "{0} 最大長度為{1}。")]
        public string Comment { get; set; }

        /// <summary>
        /// 表單圖檔
        /// </summary>
        [DisplayName("表單圖檔")]
        public ImageReqInParm FormImage { get; set; }

        [MaxLength(50)]
        public string CreateIp { get; set; }

        public int UpdateId { get; set; }

        [MaxLength(50)]
        public string UpdateIp { get; set; }
        #endregion

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CaseSexType == SexType.不詳)
            {
                yield return new ValidationResult("個案性別 有誤。", new[] { "CaseSex" });
            }

            if (CaseBirthday.HasValue && CaseBirthday >= DateTime.Today)
            {
                yield return new ValidationResult("個案生日 有誤。", new[] { "CaseBirthday" });
            }

            if (PsyHistoryType == Enum.MTC.PsyHistoryType.有)
            {
                if (Diagnose == null || !Diagnose.Any())
                    yield return new ValidationResult("過往精神病史為「有」時，診斷 欄位是必要項。", new[] { "Diagnose" });
            }

            if (PsyHistoryType == Enum.MTC.PsyHistoryType.不詳 && string.IsNullOrWhiteSpace(SymptomOther))
            {
                yield return new ValidationResult("過往精神病史為「不詳」時，精神疾病症狀-其他 欄位是必要項。", new[] { "SymptomOther" });
            }

            if (Id < 0)
            {
                yield return new ValidationResult("查無此案。", new[] { "Id" });
            }
            else if (Id == 0) //新增
            {
                if (MedicalPersonImage == null)
                    yield return new ValidationResult("醫護人員簽名圖檔 欄位是必要項。", new[] { "MedicalPersonImage" });
            }
        }
    }

    public class CNoticeSourceReqInParm : IValidatableObject
    {
        /// <summary>
        /// 通報來源(代碼)
        /// </summary>
        [Required]
        [DisplayName("通報來源")]
        public string Type { get; set; }

        /// <summary>
        /// 通報來源
        /// </summary>
        [JsonIgnore]
        [DisplayName("通報來源")]
        [Required(ErrorMessage = "{0} 代碼有誤。")]
        public SourceType? TypeName
        {
            get
            {
                if (System.Enum.TryParse(Type, out SourceType eValue))
                {
                    if (System.Enum.IsDefined(typeof(SourceType), eValue) | eValue.ToString().Contains(","))
                        return eValue;
                }
                return null;
            }
        }

        /// <summary>
        /// 通報來源_聯絡電話
        /// </summary>
        [DisplayName("通報來源-聯絡電話")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(15, ErrorMessage = "{0} 最大長度為{1}。")]
        [RegularExpression(@"^[0-9]{1,15}", ErrorMessage = "{0} 格式錯誤(必須為純數字)。")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 通報來源_姓名
        /// </summary>
        [DisplayName("通報來源-姓名")]
        [MaxLength(50, ErrorMessage = "{0} 最大長度為{1}。")]
        public string PersonName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (TypeName == SourceType.社政單位 && (string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrWhiteSpace(PersonName)))
            {
                yield return new ValidationResult("通報來源為「社政單位」時，通報人姓名 及 聯絡電話 欄位是必要項。", new[] { "Type" });
            }
        }
    }

    public class CEscortUnitReqInParm : IValidatableObject
    {
        /// <summary>
        /// 護送單位類別
        /// </summary>
        [Required]
        [DisplayName("護送單位類別")]
        public string Type { get; set; }

        /// <summary>
        /// 通報來源
        /// </summary>
        [JsonIgnore]
        [DisplayName("通報來源")]
        [Required(ErrorMessage = "{0} 代碼有誤。")]
        public EscortUnitType? TypeName
        {
            get
            {
                if (System.Enum.TryParse(Type, out EscortUnitType eValue))
                {
                    if (System.Enum.IsDefined(typeof(EscortUnitType), eValue) | eValue.ToString().Contains(","))
                        return eValue;
                }
                return null;
            }
        }

        /// <summary>
        /// 護送單位代碼, e.g. 衛生單位->醫事機構代碼
        /// </summary>
        [DisplayName("護送單位代碼")]
        [MaxLength(20, ErrorMessage = "{0} 最大長度為{1}。")]
        public string Code { get; set; }

        /// <summary>
        /// 名稱或其他說明
        /// </summary>
        [Required]
        [DisplayName("護送單位名稱或其他說明")]
        [MaxLength(20, ErrorMessage = "{0} 最大長度為{1}。")]
        public string Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Code) && (TypeName == EscortUnitType.衛生單位 || TypeName == EscortUnitType.警政單位))
            {
                yield return new ValidationResult("護送單位代碼 欄位是必要項。", new[] { "Code" });
            }
        }
    }
}
