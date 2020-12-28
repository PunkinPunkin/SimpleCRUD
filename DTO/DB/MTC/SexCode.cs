using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.DB.MTC
{
    public class SexCode
    {
        [Key]
        public int Code { get; set; }
        [MaxLength(10)]
        public string DisplayName { get; set; }
        [MaxLength(255)]
        public string Memo { get; set; }

        public bool IsDelete { get; set; } = false;
        
        public DateTime? DeleteTime { get; set; }
    }
}
