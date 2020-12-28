using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO
{
    public interface IEntity
    {
        /// <summary>
        /// 主鍵值
        /// </summary>
        int Id { get; }
        /// <summary>
        /// 建立者主鍵值
        /// </summary>
        int CreatorId { get; set; }
        /// <summary>
        /// 建立時間
        /// </summary>
        DateTime CreateDate { get; set; }
        /// <summary>
        /// 更新者主鍵值
        /// </summary>
        int UpdaterId { get; set; }
        /// <summary>
        /// 更新時間
        /// </summary>
        DateTime UpdateDate { get; set; }
    }

    public class BaseEntity : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int CreatorId { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [Required]
        public int UpdaterId { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}
