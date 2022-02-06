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

        public BuyerService(BuyerRepo buyerRepo)
        {
            _buyerrepository = buyerRepo;
        }
        public bool CreateProductBid(Buyer buyer)
        {
            var result = _buyerrepository.CreateProductBid(buyer);
            if (!result)
            {
                throw new AlreadyExistsException($"This Product {buyer.FirstName} already in use");
            }
            return result;
        }
        public bool Updateproductbids(Buyer buyer)
        {
            var result = _buyerrepository.Updateproductbids(buyer);
            if (!result)
            {
                throw new AlreadyExistsException($"This Product {buyer.FirstName} already in use");
            }
            return result;
        }
    }
}
