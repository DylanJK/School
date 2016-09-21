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
    public class BudgetItemController : Controller
    {
        //
        // GET: /BudgetItem/
        public ActionResult BudgetItem()
        {
            return View();
        }

        [HttpGet]
        public ActionResult BudgetItem(int id)
        {
            //var user = Session["user"] as User;
            //if (user == null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            Session["budgetID"] = new Budget { ID = id };

            return View();
        }


        [HttpPost]
        public ActionResult BudgetItem(string Description, int cost, string paid)
        {
            int itemIDinDatabase = 0;
            double budgetAmount = 0;

            if (ModelState.IsValid)
            {
                var budgetID = Session["budgetID"] as Budget;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WeddingsForYouDB"].ConnectionString);

                con.Open();


                //Read From the Database so I can get the Value of the BudgetAmount
                SqlCommand readBudgetAmountCMD = new SqlCommand("SELECT * FROM Budget WHERE Id=" + budgetID.ID + "", con);
                readBudgetAmountCMD.ExecuteNonQuery();

                SqlDataReader readBudgetInfo = readBudgetAmountCMD.ExecuteReader();

                while (readBudgetInfo.Read())
                {
                    itemIDinDatabase++;
                    budgetAmount = (double)readBudgetInfo[5];
                }

                readBudgetInfo.Close();

                itemIDinDatabase = itemIDinDatabase + 1;

                SqlCommand cmd = new SqlCommand("Insert into Budget values ('" + budgetID.ID + "','"+itemIDinDatabase+"','" + Description + "','" + cost + "','" + paid + "','" + budgetAmount + "')", con);
                cmd.ExecuteNonQuery();

                ViewBag.Status = "Item Submitted Successful";
            }
            else
            {
                ViewBag.Status = "Item Un-Successfully Submitted";
            }
            return View();
        }
	}
}