using BidingAPPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BidingAPPAPI.Models
{
    public class ProductBids
    {
        public Product Product { get; set; }
        public List<Buyer> Buyers { get; set; }
    }
}
