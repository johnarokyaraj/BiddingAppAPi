﻿using BidingAPPAPI.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BidingAPPAPI.Models
{
    public class Product
    {
        [SwaggerIgnore]
        public string ProductId { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string ProductName { get; set; }
        public string ShortDescription { get; set; }
        public string DetailedDescription { get; set; }
        public string Category { get; set; }
        public Int32 StartingPrice { get; set; }
        public DateTime BidEndDate { get; set; }
        public string SellerId { get; set; }

    }
}
