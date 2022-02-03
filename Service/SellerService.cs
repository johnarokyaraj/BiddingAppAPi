using BidingAPPAPI.ExceptionsResponse;
using BidingAPPAPI.Models;
using BidingAPPAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidingAPPAPI.Service
{
    public class SellerService:ISellerService
    {
        //define a private variable to represent repository
        private readonly ISellerRepo repository;

        public SellerService(SellerRepo sellerRepo)
        {
            repository = sellerRepo;
        }
        public bool CreateProduct(Product product)
        {
            var result = repository.CreateProduct(product);
            if (!result)
            {
                throw new AlreadyExistsException($"This Product {product.ProductName} already in use");
            }
            return result;
        }

    }
}
