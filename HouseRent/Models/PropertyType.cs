using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRent.Models
{
    public class PropertyType
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Property Type")]
        public string PropType { get; set; }
    }
}
