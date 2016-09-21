using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeddingsForYou.Models;

namespace WeddingsForYou.Controllers
{
    public class GuestTablesController : Controller
    {
        //
        // GET: /GuestTables/
   
        public ActionResult GuestTables()
        {

            return View();
        }
        [HttpPost]
        public ActionResult GuestTables(string TableName, string SeatAmount, string Delete)
        {
            int tableIDinDatabase = 0;
            if(ModelState.IsValid)
            {

               SqlCommand WeddingTableCMD = null;

                //Get the User session so we can get the ID
               var user = Session["user"] as User;

               SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WeddingsForYouDB"].ConnectionString);

               con.Open();

               if (TableName != null && SeatAmount != null)
               {
                    //Read From the Database so I can get the Value of the BudgetAmount
                    WeddingTableCMD = new SqlCommand("SELECT * FROM WeddingTables WHERE Id=" + user.Id + "", con);
                    WeddingTableCMD.ExecuteNonQuery();

                    SqlDataReader readWeddingTableInfo = WeddingTableCMD.ExecuteReader();

                    while (readWeddingTableInfo.Read())
                    {
                        tableIDinDatabase++;
                    }

                    readWeddingTableInfo.Close();

                    tableIDinDatabase = tableIDinDatabase + 1;

                    SqlCommand cmd = new SqlCommand("Insert into WeddingTables values ('" + user.Id + "','" + tableIDinDatabase + "','" + TableName + "','" + SeatAmount + "')", con);
                    cmd.ExecuteNonQuery();


                    ViewBag.Status = "Table Submitted Successful";
                }

                //Delete the Table from the List
                if(Delete != null)
                {
                    //I added the AND in so incase and Error comes up Here I know what I did
                    WeddingTableCMD = new SqlCommand("DELETE FROM WeddingTables WHERE tableID=" + Delete + " AND Id=" + user.Id + "", con);
                    WeddingTableCMD.ExecuteNonQuery();
                }
            }
            else
            {
                ViewBag.Status = "Table Un-Successfully Submitted";
            }
            return View();
        }
	}
}