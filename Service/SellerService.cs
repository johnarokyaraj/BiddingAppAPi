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
        private readonly ISellerRepo _sellerrepository;

        public SellerService(SellerRepo sellerRepo)
        {
            _sellerrepository = sellerRepo;
        }
        public bool CreateProduct(Product product)
        {
            var result = _sellerrepository.CreateProduct(product);
            if (!result)
            {
                throw new AlreadyExistsException($"This Product {product.ProductName} already in use");
            }
            return result;
        }

        public bool CreateSeller(Seller seller)
        {
            var result = _sellerrepository.CreateSeller(seller);
            if (!result)
            {
                throw new AlreadyExistsException($"This Product {seller.FirstName} already in use");
            }
            return result;
        }

        public ProductBids Showproductbids(Product product)
        {
            var result = _sellerrepository.Showproductbids(product);
            if (result==null)
            {
                throw new NotFoundException($"This Product {product.ProductId} not found");
            }
            return result;
        }
        public bool Deleteproduct(Product product)
        {
            var result = _sellerrepository.Deleteproduct(product);
            if (!result)
            {
                throw new AlreadyExistsException($"This Product {product.ProductName} already in use");
            }
            return result;
        }

    }
}
