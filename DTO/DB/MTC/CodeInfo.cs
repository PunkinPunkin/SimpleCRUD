using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.DB.MTC
{
    /// <summary>
    /// 系統代碼對照檔
    /// </summary>
    public class CodeInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// 類別
        /// </summary>
        [Key, Column(Order = 0)]
        [MaxLength(20)]
        public string Type { get; set; }
        /// <summary>
        /// 代碼說明
        /// </summary>
        [MaxLength(20)]
        public string Desc { get; set; }
        /// <summary>
        /// 代碼
        /// </summary>
        [Key, Column(Order = 1)]
        [MaxLength(10)]
        public string Code { get; set; }
        /// <summary>
        /// 內容
        /// </summary>
        [MaxLength(200)]
        public string Content { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public decimal? Order { get; set; }
        /// <summary>
        /// 刪除註記
        /// </summary>
        public bool IsDelete { get; set; } = false;

        public DateTime? DeleteTime { get; set; }
    }
}