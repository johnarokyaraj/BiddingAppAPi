using BidingAPPAPI.ExceptionsResponse;
using BidingAPPAPI.Models;
using BidingAPPAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<BuyerController> _logger;

        public BuyerController(BuyerService buyerService, ILogger<BuyerController> logger)
        {
            _buyerservice = buyerService;
            _logger = logger;

        }
        [Route("api/v{v:apiVersion}/buyer/place-bid")]
        [HttpPost]
        public IActionResult AddProduct([FromBody] Buyer buyer)
        {
            try
            {
                if (buyer == null)
                {
                    _logger.LogInformation("Bad Request");
                    return BadRequest();
                }
                else if (ModelState.IsValid)
                {
                    var data = _buyerservice.CreateProductBid(buyer);
                    return Ok();
                }
                else
                {
                    _logger.LogInformation("Bad Request");
                    return BadRequest();
                }
            }
            catch (AlreadyExistsException unf)
            {
                _logger.LogInformation(unf.Message.ToString());
                return Unauthorized(unf.Message);
            }
            catch
            {
                _logger.LogInformation("Some server error");
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
                    _logger.LogInformation("Bad Request");
                    return BadRequest();
                }
                else if (ModelState.IsValid)
                {
                    var data = _buyerservice.Updateproductbids(buyer);
                    return Ok();
                }
                else
                {
                    _logger.LogInformation("Bad Request");
                    return BadRequest();
                }
            }
            catch (AlreadyExistsException unf)
            {
                _logger.LogInformation(unf.Message.ToString());
                return Unauthorized(unf.Message);
            }
            catch
            {
                _logger.LogInformation("Some server error");
                return StatusCode(500, "Some server error");
            }
        }
    }
}
