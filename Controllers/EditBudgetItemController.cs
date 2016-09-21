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
    public class EditBudgetItemController : Controller
    {
        //
        // GET: /EditBudgetItem/
        public ActionResult EditBudgetItem()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditBudgetItem(int itemID)
        {
            
            Session["editBudgetItemID"] = new Budget { ItemID = itemID };

            return View();
        }

        [HttpPost]
        public ActionResult EditBudgetItem(string description, string cost, string paid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WeddingsForYouDB"].ConnectionString);

            con.Open();

            //Get Session of Budget
            var budgetID = Session["editBudgetItemID"] as Budget;

            //Get the Session of the UserID
            var user = Session["user"] as User;

            if (ModelState.IsValid)
            {
                //Updates the Current Item Witht the New Information in the Database
                //I Think this is Wrong make an AND Statement to get the userID too
                SqlCommand cmd = new SqlCommand("UPDATE Budget SET Description='" + description + "', Cost='" + cost + "', Paid='" + paid + "' WHERE itemID=" + budgetID.ItemID + " AND Id="+user.Id+"", con);
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