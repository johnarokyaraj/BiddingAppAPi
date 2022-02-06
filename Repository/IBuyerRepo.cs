using BidingAPPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidingAPPAPI.Repository
{
    interface IBuyerRepo
    {
        bool CreateProductBid(Buyer buyer);
        bool Updateproductbids(Buyer buyer);

    }
}
