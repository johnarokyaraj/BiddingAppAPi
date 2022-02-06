using BidingAPPAPI.Models;
using BidingAPPAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidingAPPAPI.Controllers
{
    [ApiVersion("v1")]
    [ApiController]
    public class BuyerController : ControllerBase
    {
        private readonly IBuyerService _buyerservice;

        public BuyerController(BuyerService buyerService)
        {
            _buyerservice = buyerService;
        }
        [Route("api/{v:apiVersion}/buyer/place-bid")]
        [HttpPost]
        public IActionResult AddProduct([FromBody] Buyer buyer)
        {
            var data = _buyerservice.CreateProductBid(buyer);
            return Ok();
        }
        [Route("api/{v:apiVersion}/buyer/update-bid/{productId}/{buyerEmailld}/{newBidAmount}")]
        [HttpPost]
        public IActionResult updateBuyerProductbid([FromBody] Buyer buyer)
        {
            var data = _buyerservice.Updateproductbids(buyer);
            return Ok();
        }
    }
}
