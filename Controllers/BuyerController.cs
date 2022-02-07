using BidingAPPAPI.ExceptionsResponse;
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
    [ApiVersion("1")]
    [ApiController]
    public class BuyerController : ControllerBase
    {
        private readonly IBuyerService _buyerservice;

        public BuyerController(BuyerService buyerService)
        {
            _buyerservice = buyerService;
        }
        [Route("api/v{v:apiVersion}/buyer/place-bid")]
        [HttpPost]
        public IActionResult AddProduct([FromBody] Buyer buyer)
        {
            try
            {
                if (buyer == null)
                {
                    return BadRequest();
                }
                else if (ModelState.IsValid)
                {
                    var data = _buyerservice.CreateProductBid(buyer);
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (AlreadyExistsException unf)
            {
                return Unauthorized(unf.Message);
            }
            catch
            {
                return StatusCode(500, "Some server error");
            }
        }
        [Route("api/v{v:apiVersion}/buyer/update-bid/{productId}/{buyerEmailld}/{newBidAmount}")]
        [HttpPost]
        public IActionResult updateBuyerProductbid(string productId,string buyerEmailld, string newBidAmount)
        {
            try
            {
                Buyer buyer = new Buyer { ProductId = productId, Email = buyerEmailld, BiddingAmount = newBidAmount };
                if (buyer == null)
                {
                    return BadRequest();
                }
                else if (ModelState.IsValid)
                {
                    var data = _buyerservice.Updateproductbids(buyer);
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (AlreadyExistsException unf)
            {
                return Unauthorized(unf.Message);
            }
            catch
            {
                return StatusCode(500, "Some server error");
            }
        }
    }
}
