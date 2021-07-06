using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRent.Models
{
    public class Booking
    {
        public Booking()
        {
            BookingDetail = new List<BookingDetail>();
            BookingDetail2 = new List<BookingDetail2>();
        }
        public int Id { get; set; }

        [Display(Name = "Booking No")]
        public string BookingNo { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Phoner No")]
        public string PhoneNo { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual List<BookingDetail> BookingDetail { get; set; }
        public virtual List<BookingDetail2> BookingDetail2 { get; set; }

    }
}
