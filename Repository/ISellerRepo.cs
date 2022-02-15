using BidingAPPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidingAPPAPI.Repository
{
    interface ISellerRepo
    {
        bool CreateProduct(Product product);
        bool CreateSeller(Seller seller);
        ProductBids Showproductbids(Product product);
        bool Deleteproduct(Product product);
        Product GetProduct(Product product);
        List<Product> GetProducts();
        List<Buyer> GetproductBids(string prodcutId, int pageNo, int pageSize, string sortColumn, string sortOrder);

    }
}
