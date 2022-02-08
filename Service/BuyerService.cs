using BidingAPPAPI.ExceptionsResponse;
using BidingAPPAPI.Models;
using BidingAPPAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidingAPPAPI.Service
{
    public class BuyerService:IBuyerService
    {
        //define a private variable to represent repository
        private readonly IBuyerRepo _buyerrepository;

        private readonly ISellerRepo _sellerrepository;

        public BuyerService(BuyerRepo buyerRepo,SellerRepo sellerRepo)
        {
            _buyerrepository = buyerRepo;
            _sellerrepository = sellerRepo;

        }
        public bool CreateProductBid(Buyer buyer)
        {
            Product product = new Product { ProductId=buyer.ProductId };
            var productSel = _sellerrepository.GetProduct(product);
            var result = _buyerrepository.CreateProductBid(buyer);
            if (!result)
            {
                throw new AlreadyExistsException($"This Product {productSel.ProductName}  bid already in placed");
            }
            return result;
        }
        public bool Updateproductbids(Buyer buyer)
        {
            Product product = new Product { ProductId = buyer.ProductId };
            var productSel = _sellerrepository.GetProduct(product);
            var result = _buyerrepository.Updateproductbids(buyer);
            if (!result)
            {
                throw new AlreadyExistsException($"This Product {productSel.ProductName} bid cannot be updated");
            }
            return result;
        }
    }
}
