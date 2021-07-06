using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRent.Models
{
    public class BookingDetail2
    {
        public int Id { get; set; }
        [Display(Name = "Booking No")]
        public int BookingId { get; set; }
        [Display(Name = "DetailOfDuplex")]
        public int DetailOfDuplexId { get; set; }
        [ForeignKey("BookingId")]
        public Booking booking { get; set; }
        [ForeignKey("DetailOfDuplexId")]
        public DetailOfDuplex detailOfDuplex { get; set; }
    }
}
