using BidingAPPAPI.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BidingAPPAPI.Models
{
    public class Buyer: PagingFilter
    {
        [SwaggerIgnore]
        public string BuyerId { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Name length can't be more than 30 and less than 5.")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Name length can't be more than 30 and less than 5.")]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [RegularExpression(@"^\d{6}$")]
        public string Pin { get; set; }
        [Required]
        [Phone]
        [StringLength(10)]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string ProductId { get; set; }

        public string BiddingAmount { get; set; }

    }
}
