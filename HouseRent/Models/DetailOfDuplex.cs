using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRent.Models
{
    public class DetailOfDuplex
    {
        public int Id { get; set; }
        [Required]
        public string DuplexName { get; set; }
       
        [Required]
        public int RoomNumber { get; set; }
        [Required]
        public int BathNumber { get; set; }
        [Required]
        public bool CarParking { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Photo { get; set; }
    }
}
