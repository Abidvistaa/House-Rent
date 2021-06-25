using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRent.Models
{
    public class FlatDetail
    {
        public int Id { get; set; }
        [Required]
        
        public int FlatId { get; set; }
        [ForeignKey("FlatId")]
        public Flat Flat { get; set; }
        [Required]
        public int RoomNumber { get; set; }
        public int BathNumber { get; set; }
        public bool CarParking { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }

    }
}
