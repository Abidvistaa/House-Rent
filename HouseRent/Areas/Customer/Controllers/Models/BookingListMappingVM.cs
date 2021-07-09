using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRent.Areas.Customer.Models
{
    public class BookingListMappingVm
    {
        
        public string BookingNo { get; set; }
        public string Name { get; set; }
        public string PhoneNO { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string FlatName { get; set; }
        public string DuplexName { get; set; }
        public string Photo { get; set; }

    }
}
