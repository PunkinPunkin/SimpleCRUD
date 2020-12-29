using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO.ReqInParm.MTC
{
    public class PoliceStationReqInParm
    {
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
    }
}
