using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.DB.MTC
{
    /// <summary>
    /// 警政單位
    /// </summary>
    public class PoliceStation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(10)]
        public string Zip { get; set; }
        [MaxLength(200)]
        public string Address { get; set; }
        [MaxLength(20)]
        public string Tel { get; set; }
        [Required]
        public bool IsDeleted { get; set; } = false;
        [Required]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}
