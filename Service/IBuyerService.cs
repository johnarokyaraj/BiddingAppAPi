using BidingAPPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidingAPPAPI.Service
{
    interface IBuyerService
    {
        bool CreateProductBid(Buyer buyer);
        bool Updateproductbids(Buyer buyer);

    }
}
