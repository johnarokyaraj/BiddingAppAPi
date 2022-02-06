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
            var data = _sellerservice.CreateProduct(product);
            return Ok();
        }
        [HttpPost]
        [Route("api/v{v:apiVersion}/seller/add-seller")]
        public IActionResult AddSellerInfo([FromBody]Seller seller)
        {
            var data = _sellerservice.CreateSeller(seller);

            return Ok();
        }
        [Route("api/v{v:apiVersion}/seller/show-bids/{productId}")]
        [HttpGet]
        public IActionResult Showproductbids(string productId)
        {
            var product = new Product { ProductId = productId };
            var data = _sellerservice.CreateProduct(product);
            return Ok();
        }
        [Route("api/v{v:apiVersion}/seller/delete/{productId}")]
        [HttpGet]
        public IActionResult Deleteproduct(string productId)
        {
            var product = new Product { ProductId = productId };
            var data = _sellerservice.CreateProduct(product);
            return Ok();
        }

    }
}
