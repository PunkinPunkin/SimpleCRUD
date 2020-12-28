using Shared.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.DB.MTC
{
    [Table("ToHospitalRecords")]
    public class MentalillnessToHospitalRecord : BaseEntity
    {
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
        [MaxLength(100)]
        [DisplayName("通報來源")]
        public string Sources { get; set; }

        /// <summary>
        /// 通報來源-姓名
        /// </summary>
        [DisplayName("通報來源-姓名")]
        [MaxLength(50)]
        public string SourceName { get; set; }

        /// <summary>
        /// 通報來源-聯絡電話
        /// </summary>
        [DisplayName("通報來源-聯絡電話")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(15)]
        [RegularExpression(@"^[0-9]{1,15}")]
        public string SourcePhoneNumber { get; set; }

        /// <summary>
        /// 個案姓名
        /// </summary>
        [Required]
        [DisplayName("個案姓名")]
        [MaxLength(50)]
        public string CaseName { get; set; }

        /// <summary>
        /// 個案身分證/居留證
        /// </summary>
        [Required]
        [DisplayName("個案身分證/居留證")]
        [MaxLength(20)]
        [TaiwanIdentityCardNumber(ErrorMessage = "身分證字號格式不符。")]
        public string CaseIdentityNumber { get; set; }

        /// <summary>
        /// 個案性別(1: 女, 2: 男, 3: 不詳)
        /// </summary>
        [Required]
        [DisplayName("個案性別")]
        [MaxLength(1)]
        public string CaseSex { get; set; }

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
        [MaxLength(15)]
        [RegularExpression(@"^[0-9]{1,15}")]
        public string CasePhoneNumber { get; set; }

        /// <summary>
        /// 個案聯絡地址
        /// </summary>
        [Required]
        [DisplayName("個案聯絡地址")]
        [MaxLength(200)]
        public string CaseAddress { get; set; }

        /// <summary>
        /// 事發地點-行政區
        /// </summary>
        [Required]
        [DisplayName("事發地點-行政區")]
        [MaxLength(50)]
        public string LocationArea { get; set; }

        /// <summary>
        /// 事發地點
        /// </summary>
        [Required]
        [DisplayName("事發地點")]
        [MaxLength(200)]
        public string Location { get; set; }

        /// <summary>
        /// 出席家屬姓名
        /// </summary>
        [Required]
        [DisplayName("出席家屬姓名")]
        [MaxLength(50)]
        public string AttenderName { get; set; }

        /// <summary>
        /// 出席者聯絡電話
        /// </summary>
        [Required]
        [DisplayName("出席者聯絡電話")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(15)]
        [RegularExpression(@"^[0-9]{1,15}")]
        public string AttenderPhoneNumber { get; set; }

        /// <summary>
        /// 與個案關係(代碼)
        /// </summary>
        [Required]
        [DisplayName("與個案關係")]
        [MaxLength(10)]
        public string Relationship { get; set; }

        /// <summary>
        /// 護送單位
        /// </summary>
        [Required]
        [DisplayName("護送單位")]
        public virtual ICollection<EscortUnit> EscortUnits { get; set; }

        /// <summary>
        /// 主護送人
        /// </summary>
        [Required]
        [DisplayName("主護送人")]
        [MaxLength(100)]
        public string EscortMain { get; set; }

        /// <summary>
        /// 協助人
        /// </summary>
        [DisplayName("協助人")]
        [MaxLength(50)]
        public string EscortSup { get; set; }

        /// <summary>
        /// 協助內容
        /// </summary>
        [DisplayName("協助內容")]
        [MaxLength(20)]
        public string SupportContent { get; set; }

        /// <summary>
        /// 是否親自護送(true: 親自護送就醫, false: 協助送醫)
        /// </summary>
        [Required]
        [DisplayName("是否親自護送")]
        public bool InPerson { get; set; }

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
        [MaxLength(20)]
        public string EmergencyAgencyId { get; set; }

        /// <summary>
        /// 過往精神疾病史(有=1, 無=2, 不詳=3)
        /// </summary>
        [Required]
        [DisplayName("過往精神疾病史")]
        public int PsyHistory { get; set; }

        /// <summary>
        /// 診斷-代碼
        /// </summary>
        [DisplayName("診斷-代碼")]
        [MaxLength(30)]
        public string Diagnose { get; set; }

        /// <summary>
        /// 診斷-其他
        /// </summary>
        [DisplayName("診斷-其他")]
        [MaxLength(30)]
        public string DiagnoseOther { get; set; }

        /// <summary>
        /// 精神疾病症狀
        /// </summary>
        [MaxLength(100)]
        [DisplayName("精神疾病症狀")]
        public string Symptoms { get; set; }

        /// <summary>
        /// 精神疾病症狀-其他
        /// </summary>
        [MaxLength(50)]
        [DisplayName("精神疾病症狀-其他")]
        public string SymptomOther { get; set; }

        /// <summary>
        /// 就醫醫院_代碼
        /// </summary>
        [Required]
        [DisplayName("就醫醫院")]
        [MaxLength(50)]
        public string Hospital { get; set; }

        /// <summary>
        /// 醫護人員簽名圖檔
        /// </summary>
        [Required]
        [DisplayName("醫護人員簽名圖檔")]
        public Guid MedicalPersonImage { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        [DisplayName("備註")]
        [MaxLength(500)]
        public string Comment { get; set; }

        [DisplayName("表單圖檔")]
        public Guid? FormImage { get; set; }

        /// <summary>
        /// 介接時間
        /// </summary>
        public DateTime IntfDate { get; set; } = DateTime.Now;

        [MaxLength(50)]
        public string CreateIp { get; set; }

        [MaxLength(50)]
        public string UpdateIp { get; set; }
    }

    [Table("ToHospitalUnits")]
    public class EscortUnit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int HeaderId { get; set; }

        /// <summary>
        /// 護送單位類別
        /// </summary>
        [Required]
        [DisplayName("護送單位類別")]
        public int Type { get; set; }

        /// <summary>
        /// 護送單位代碼, e.g. 衛生單位->醫事機構代碼
        /// </summary>
        [DisplayName("護送單位代碼")]
        [MaxLength(20)]
        public string Code { get; set; }

        /// <summary>
        /// 護送單位名稱
        /// </summary>
        [Required]
        [DisplayName("護送單位名稱")]
        [MaxLength(20)]
        public string Name { get; set; }

        [ForeignKey("HeaderId")]
        public virtual MentalillnessToHospitalRecord Header { get; set; }
    }
}
