using BidingAPPAPI.ExceptionsResponse;
using BidingAPPAPI.Models;
using BidingAPPAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Microsoft.Extensions.Logging;

namespace BidingAPPAPI.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISellerService _sellerservice;
        private readonly ILogger<SellerController> _logger;
       
        public SellerController(SellerService sellerService, ILogger<SellerController> logger)
        {
            _sellerservice = sellerService;
            _logger = logger;

        }
        [Route("api/v{v:apiVersion}/seller/add-product")]
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            try
            {
                if (product == null)
                {
                    _logger.LogInformation("Bad Request");
                    return BadRequest();
                }
                else if (ModelState.IsValid)
                {
                    var data = _sellerservice.CreateProduct(product);
                    return Ok();
                }
                else
                {
                    _logger.LogInformation("Bad Request");
                    return BadRequest();
                }
            }
            catch (NotSavedException unf)
            {
                _logger.LogInformation(unf.Message);

                return Unauthorized(unf.Message);
            }
            catch
            {
                _logger.LogInformation("Some server error");
                return StatusCode(404, "Some server error");
            }
        }
        [HttpPost]
        [Route("api/v{v:apiVersion}/seller/add-seller")]
        public IActionResult AddSellerInfo([FromBody]Seller seller)
        {
            try
            {
                if (seller == null)
                {
                    return BadRequest();
                }
                else if (ModelState.IsValid)
                {
                    var data = _sellerservice.CreateSeller(seller);
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (NotSavedException unf)
            {
                return Unauthorized(unf.Message);
            }
            catch
            {
                return StatusCode(404, "Some server error");
            }
        }
        [Route("api/v{v:apiVersion}/seller/show-bids/{productId}")]
        [HttpGet]
        public IActionResult Showproductbids(string productId)
        {
            try
            {
                var product = new Product { ProductId = productId };
                var data = _sellerservice.Showproductbids(product);
                return StatusCode((int)HttpStatusCode.OK,data);
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message.ToString());
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message.ToString());
            }
        }
        [Route("api/v{v:apiVersion}/seller/delete/{productId}")]
        [HttpDelete]
        public IActionResult Deleteproduct(string productId)
        {
            try
            {
                var product = new Product { ProductId = productId };
                var data = _sellerservice.Deleteproduct(product);
                return Ok();
            }
            catch (ActionNotAllowedException unf)
            {
                _logger.LogInformation(unf.Message.ToString());

                return Unauthorized(unf.Message);
            }
            catch
            {
                _logger.LogInformation("Some server error");
                return StatusCode(404, "Some server error");
            }
        }
        [Route("api/v{v:apiVersion}/seller/GetProducts")]
        [HttpGet]
        public IActionResult Getproducts()
        {
            try
            {
                var data = _sellerservice.GetProducts();
                return StatusCode((int)HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message.ToString());
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message.ToString());
            }
        }
        [Route("api/v{v:apiVersion}/seller/GetProductBids/{productId}/{pageNo}/{pageSize}/{sortColumn}/{sortOrder}")]
        [HttpGet]
        public IActionResult GetproductBids(string prodcutId,  int pageNo=1,  int pageSize=5,  string sortColumn=null, string sortOrder="ASC")
        {
            try
            {
                var data = _sellerservice.GetproductBids(prodcutId,pageNo,pageSize,sortColumn,sortOrder);
                return StatusCode((int)HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message.ToString());
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message.ToString());
            }
        }

    }
}
