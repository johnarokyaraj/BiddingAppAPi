using BidingAPPAPI.ExceptionsResponse;
using BidingAPPAPI.Models;
using BidingAPPAPI.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BidingAPPAPI.QueryService
{
    public static  class QueryProductBiddingService
    {
        public class Query : IRequest<ProductBids> { }
        public class Handler : IRequestHandler<Query, ProductBids>
        {
            public SellerRepo sellerRepo { get; set; }
            public Handler(SellerRepo SellerRepo)
            {
                sellerRepo = SellerRepo;
            }
            public async Task<ProductBids> Handle(Query request, CancellationToken cancellationToken)
            {
                var prodResult = sellerRepo.GetProduct(request);
                var result = await sellerRepo.Showproductbids(request);
                if (result == null)
                {
                    throw new NotFoundException($"This Product {prodResult?.ProductName} not found");
                }
                return result;
            }
        }
    }
}
