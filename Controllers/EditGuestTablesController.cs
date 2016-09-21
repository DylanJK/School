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
    public class EditGuestTablesController : Controller
    {
        //
        // GET: /EditGuestTables/
        [HttpGet]
        public ActionResult EditGuestTables(int tableID)
        {
            Session["tableID"] = new WeddingTables { TableID = tableID };
            return View();
        }
        [HttpPost]
        public ActionResult EditGuestTables(string TableName, string SeatAmount)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WeddingsForYouDB"].ConnectionString);

            con.Open();

            //Get the Session of the GuestID
            var table = Session["tableID"] as WeddingTables;

            //Get the Session of the UserID
            var user = Session["user"] as User;

            if (ModelState.IsValid)
            {
                //Updates the Current Item Witht the New Information in the Database
                SqlCommand cmd = new SqlCommand("UPDATE WeddingTables SET tableName='" + TableName + "', tableSeatsAmount='" + SeatAmount + "' WHERE tableID=" + table.TableID + " AND Id=" + user.Id + "", con);
                cmd.ExecuteNonQuery();

                ViewBag.Status = "Update Successful";
            }
            else
            {
                ViewBag.Status = "Update Un-Successful";
            }


            return View();
        }
	}
}