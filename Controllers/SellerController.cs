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
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISellerService _service;

        public SellerController(SellerService sellerService)
        {
            _service = sellerService;
        }
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            var data = _service.CreateProduct(product);
            return Ok();
        }
        [HttpPost]
        public IActionResult AddSellerInfo()
        {
            return Ok();
        }
    }
}
