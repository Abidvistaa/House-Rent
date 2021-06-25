using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRent.Models
{
    public class Flat
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Floor Number")]
        public string Floor { get; set; }
    }
}
