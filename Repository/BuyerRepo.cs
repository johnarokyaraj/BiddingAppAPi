using BidingAPPAPI.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace BidingAPPAPI.Repository
{
    public class BuyerRepo:IBuyerRepo
    {
        private readonly IConfiguration m_config;

        public BuyerRepo(IConfiguration config)
        {
            m_config = config;
        }

        public bool CreateProductBid(Buyer buyer)
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
                    cmds.CommandText = "[dbo].[USP_SaveBuyerInfo]";
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
                    dt.Columns.Add("ProductId", typeof(string));
                    dt.Columns.Add("BidAmount", typeof(string));

                    var row = dt.NewRow();

                    row["FirstName"] = buyer.FirstName;
                    row["LastName"] = buyer.LastName;
                    row["Address"] = buyer.Address;
                    row["City"] = buyer.City;
                    row["State"] = buyer.State;
                    row["Pin"] = buyer.Pin;
                    row["Phone"] = buyer.Phone;
                    row["Email"] = buyer.Email;
                    row["ProductId"] = buyer.ProductId;
                    row["BidAmount"] = buyer.BiddingAmount;

                    dt.Rows.Add(row);
                    //

                    //populate your Datatable
                    SqlParameter param = new SqlParameter("@buyerinfo", SqlDbType.Structured)
                    {
                        TypeName = "[dbo].[UT_BuyerInfo]",
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
                if (result>0)
                {
                    return true;
                }
                else {
                    return false;
                }
            }
            catch {
                return false;
            }
        }

        public bool Updateproductbids(Buyer buyer)
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
                    cmds.CommandText = "[dbo].[USP_UpdateProductBids]";
                    cmds.CommandType = CommandType.StoredProcedure;
                    //params
                    cmds.Parameters.Add("@productId", SqlDbType.VarChar).Value = buyer.ProductId;
                    cmds.Parameters.Add("@Email", SqlDbType.VarChar).Value = buyer.Email;
                    cmds.Parameters.Add("@BidAmount", SqlDbType.VarChar).Value = buyer.BiddingAmount;

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
            catch {
                return false;
            }
        }
    }
}
