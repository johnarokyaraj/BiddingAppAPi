using BidingAPPAPI.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BidingAPPAPI.Repository
{
    public class SellerRepo : ISellerRepo
    {
        private readonly IConfiguration m_config;
        public SellerRepo(IConfiguration config)
        {
            m_config = config;
        }
        public bool CreateProduct(Product product)
        {

            try
            {
                DataTable dtdb = new DataTable();
                //you can get connection string as follows
                string connectionString = m_config.GetConnectionString("SqlConnectionString");
                using (SqlConnection cons = new SqlConnection(connectionString))
                {
                    cons.Open();
                    SqlCommand cmds = new SqlCommand();
                    cmds.Connection = cons;
                    cmds.CommandText = "[dbo].[USP_CreateProduct]";
                    cmds.CommandType = CommandType.StoredProcedure;

                    //Table Type
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ProductName", typeof(string));
                    dt.Columns.Add("ShortDescription", typeof(string));
                    dt.Columns.Add("DetailedDescription", typeof(string));
                    dt.Columns.Add("Category", typeof(string));
                    dt.Columns.Add("StartingPrice", typeof(string));
                    dt.Columns.Add("BidEndDate", typeof(DateTime));
                    dt.Columns.Add("SellerId", typeof(string));

                    var row = dt.NewRow();

                    row["ProductName"] = product.ProductName;
                    row["ShortDescription"] = product.ShortDescription;
                    row["DetailedDescription"] = product.DetailedDescription;
                    row["Category"] = product.Category;
                    row["StartingPrice"] = product.StartingPrice;
                    row["BidEndDate"] = Convert.ToDateTime(product.BidEndDate);
                    row["SellerId"] = product.SellerId;

                    dt.Rows.Add(row);
                    //

                    //populate your Datatable
                    SqlParameter param = new SqlParameter("@product", SqlDbType.Structured)
                    {
                        TypeName = "[dbo].[UT_Product]",
                        Value = dt
                    };
                    cmds.Parameters.Add(param);
                    //
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmds))
                    {
                        adp.Fill(dtdb);
                    }
                    cons.Close();
                }
                int result = (from DataRow m in dtdb.Rows where m.Field<bool>("StatusCode") == true select m).Count();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool CreateSeller(Seller seller)
        {

            try
            {
                DataTable dtdb = new DataTable();
                //you can get connection string as follows
                string connectionString = m_config.GetConnectionString("SqlConnectionString");
                using (SqlConnection cons = new SqlConnection(connectionString))
                {
                    cons.Open();
                    SqlCommand cmds = new SqlCommand();
                    cmds.Connection = cons;
                    cmds.CommandText = "[dbo].[USP_SaveSellerInfo]";
                    cmds.CommandType = CommandType.StoredProcedure;

                    //Table Type
                    DataTable dt = new DataTable();
                    dt.Columns.Add("FirstName", typeof(string));
                    dt.Columns.Add("LastName", typeof(string));
                    dt.Columns.Add("Address", typeof(string));
                    dt.Columns.Add("City", typeof(string));
                    dt.Columns.Add("State", typeof(string));
                    dt.Columns.Add("Pin", typeof(string));
                    dt.Columns.Add("Phone", typeof(string));
                    dt.Columns.Add("Email", typeof(string));
                    

                    var row = dt.NewRow();

                    row["FirstName"] = seller.FirstName;
                    row["LastName"] = seller.LastName;
                    row["Address"] = seller.Address;
                    row["City"] = seller.City;
                    row["State"] = seller.State;
                    row["Pin"] = seller.Pin;
                    row["Phone"] = seller.Phone;
                    row["Email"] = seller.Email;

                    dt.Rows.Add(row);
                    //

                    //populate your Datatable
                    SqlParameter param = new SqlParameter("@sellerinfo", SqlDbType.Structured)
                    {
                        TypeName = "[dbo].[UT_SellerInfo]",
                        Value = dt
                    };
                    cmds.Parameters.Add(param);
                    //
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmds))
                    {
                        adp.Fill(dtdb);
                    }
                    cons.Close();
                }
                int result = (from DataRow m in dtdb.Rows where m.Field<bool>("StatusCode") == true select m).Count();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public ProductBids Showproductbids(Product product)
        {
            try
            {
                ProductBids productBids = new ProductBids();
                DataSet dsdb = new DataSet();
                //you can get connection string as follows
                string connectionString = m_config.GetConnectionString("SqlConnectionString");
                using (SqlConnection cons = new SqlConnection(connectionString))
                {
                    cons.Open();
                    SqlCommand cmds = new SqlCommand();
                    cmds.Connection = cons;
                    cmds.CommandText = "[dbo].[USP_ShowProductBids]";
                    cmds.CommandType = CommandType.StoredProcedure;
                    //params
                    cmds.Parameters.Add("@productId", SqlDbType.VarChar).Value = product.ProductId;

                    //
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmds))
                    {
                        adp.Fill(dsdb);
                    }
                    cons.Close();
                }
                if (!IsEmpty(dsdb))
                {
                    DataTable dtProduct = dsdb.Tables[0].Rows.Count > 0 ? dsdb.Tables[0] : null;
                    DataTable dtBuyers = dsdb.Tables[1].Rows.Count>0 ? dsdb.Tables[1]:null;

                    var product1 = from d in dtProduct.AsEnumerable()
                                select new Product
                                {
                                    ProductId = d.Field<string>("ProductId"),
                                    ProductName = d.Field<string>("ProductName"),
                                    ShortDescription = d.Field<string>("ProductName"),
                                    DetailedDescription = d.Field<string>("ProductName"),
                                    Category = d.Field<string>("ProductName"),
                                    StartingPrice = d.Field<string>("ProductName"),
                                    BidEndDate = d.Field<DateTime>("BidEndDate"),
                                    SellerId = d.Field<string>("ProductName")
                                };
                    var buyers =( from d in dtBuyers.AsEnumerable()
                              select new Buyer
                              {
                                  BuyerId= d.Field<string>("BuyerId"),
                                  FirstName = d.Field<string>("FirstName"),
                                  LastName = d.Field<string>("LastName"),
                                  Address = d.Field<string>("Address"),
                                  City = d.Field<string>("City"),
                                  State = d.Field<string>("State"),
                                  Pin = d.Field<string>("Pin"),
                                  Phone = d.Field<string>("Phone"),
                                  Email = d.Field<string>("Email"),
                                  ProductId = d.Field<string>("ProductId"),
                                  BiddingAmount = d.Field<string>("BidAmount"),
                              }).ToList();
                    productBids = new ProductBids { 
                    Product= product1.FirstOrDefault(),
                    Buyers= buyers
                    };
                    return productBids;
                }
                else {
                    return null;
                }

            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public bool Deleteproduct(Product product)
        {
            try
            {
                DataTable dtdb = new DataTable();
                //you can get connection string as follows
                string connectionString = m_config.GetConnectionString("SqlConnectionString");
                using (SqlConnection cons = new SqlConnection(connectionString))
                {
                    cons.Open();
                    SqlCommand cmds = new SqlCommand();
                    cmds.Connection = cons;
                    cmds.CommandText = "[dbo].[USP_DeleteProduct]";
                    cmds.CommandType = CommandType.StoredProcedure;
                    //params
                    cmds.Parameters.Add("@productId", SqlDbType.VarChar).Value = product.ProductId;

                    //
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmds))
                    {
                        adp.Fill(dtdb);
                    }
                    cons.Close();
                }
                int result = (from DataRow m in dtdb.Rows where m.Field<bool>("StatusCode") == true select m).Count();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public Product GetProduct(Product product)
        {
            try
            {
                DataSet dsdb = new DataSet();
                //you can get connection string as follows
                string connectionString = m_config.GetConnectionString("SqlConnectionString");
                using (SqlConnection cons = new SqlConnection(connectionString))
                {
                    cons.Open();
                    SqlCommand cmds = new SqlCommand();
                    cmds.Connection = cons;
                    cmds.CommandText = "[dbo].[USP_ShowProduct]";
                    cmds.CommandType = CommandType.StoredProcedure;
                    //params
                    cmds.Parameters.Add("@productId", SqlDbType.VarChar).Value = product.ProductId;

                    //
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmds))
                    {
                        adp.Fill(dsdb);
                    }
                    cons.Close();
                }
                if (!IsEmpty(dsdb))
                {
                    DataTable dtProduct = dsdb.Tables[0].Rows.Count > 0 ? dsdb.Tables[0] : null;

                    var product1 = from d in dtProduct.AsEnumerable()
                                   select new Product
                                   {
                                       ProductId = d.Field<string>("ProductId"),
                                       ProductName = d.Field<string>("ProductName"),
                                       ShortDescription = d.Field<string>("ProductName"),
                                       DetailedDescription = d.Field<string>("ProductName"),
                                       Category = d.Field<string>("ProductName"),
                                       StartingPrice = d.Field<string>("ProductName"),
                                       BidEndDate = d.Field<DateTime>("BidEndDate"),
                                       SellerId = d.Field<string>("ProductName")
                                   };
                   
                   
                    return product1.FirstOrDefault();
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        bool IsEmpty(DataSet dataSet)
        {
        if (dataSet.Tables[0].Rows.Count != 0) return false;

            return true;
        }
    }
}
