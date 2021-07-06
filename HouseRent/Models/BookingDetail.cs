using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRent.Models
{
    public class BookingDetail
    {
        public int Id { get; set; }
        [Display(Name ="Booking No")]
        public int BookingId { get; set; }
        [Display(Name = "DetailOfFlat")]
        public int DetailOfFlatId { get; set; }
        [ForeignKey("BookingId")]
        public Booking booking { get; set; }
        [ForeignKey("DetailOfFlatId")]
        public DetailOfFlat detailOfFlat { get; set; }
    }
}
