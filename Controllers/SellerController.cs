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
namespace BidingAPPAPI.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISellerService _sellerservice;

        public SellerController(SellerService sellerService)
        {
            _sellerservice = sellerService;
        }
        [Route("api/v{v:apiVersion}/seller/add-product")]
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest();
                }
                else if (ModelState.IsValid)
                {
                    var data = _sellerservice.CreateProduct(product);
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
            catch (AlreadyExistsException unf)
            {
                return Unauthorized(unf.Message);
            }
            catch
            {
                return StatusCode(500, "Some server error");
            }
        }
        [Route("api/v{v:apiVersion}/seller/show-bids/{productId}")]
        [HttpGet]
        public HttpResponseMessage Showproductbids(string productId)
        {
            try
            {
                var product = new Product { ProductId = productId };
                var data = _sellerservice.Showproductbids(product);
                data.StatusCode = System.Net.HttpStatusCode.OK;
                return data;
            }
            catch
            {
                return new ProductBids { StatusCode = System.Net.HttpStatusCode.InternalServerError };
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
