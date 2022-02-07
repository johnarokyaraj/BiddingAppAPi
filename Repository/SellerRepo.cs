using BidingAPPAPI.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidingAPPAPI.Repository
{
    public class SellerRepo : ISellerRepo
    {
        private readonly IConfiguration m_config;
        public SellerRepo(IConfiguration config)
        {
            m_config = config;
        }
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
