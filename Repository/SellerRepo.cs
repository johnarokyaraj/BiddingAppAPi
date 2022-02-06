using BidingAPPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidingAPPAPI.Repository
{
    public class SellerRepo : ISellerRepo
    {
        public bool CreateProduct(Product product)
        {
            return true;
        }

        public bool CreateSeller(Seller seller)
        {
            return true;
        }

        public bool Showproductbids(Product product)
        {
            return true;
        }
        public bool Deleteproduct(Product product)
        {
            return true;
        }

    }
}
