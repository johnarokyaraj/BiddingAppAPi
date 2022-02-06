﻿using BidingAPPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidingAPPAPI.Repository
{
    interface ISellerRepo
    {
        bool CreateProduct(Product product);
        bool CreateSeller(Seller seller);
        bool Showproductbids(Product product);
        bool Deleteproduct(Product product);

    }
}