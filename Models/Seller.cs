using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BidingAPPAPI.Models
{
    public class Seller
    {
        [Required]
        [StringLength(30,MinimumLength =5)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pin { get; set; }
        [Required]
        [Phone]
        [StringLength(10)]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
