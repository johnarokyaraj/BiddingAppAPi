using BidingAPPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidingAPPAPI.Service
{
    interface ISellerService
    {
        bool CreateProduct(Product product);
    }
}
