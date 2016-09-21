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
    public class BudgetController : Controller
    {
        //
        // GET: /Budget/

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WeddingsForYouDB"].ConnectionString);


        [HttpGet]
        public ActionResult Budget()
        {
           

            var user = Session["user"] as User;
            if(user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Budget(string Delete, string Update, string Paid, string UnPaid, string All, string UnCheck, string budgetAmount)
        {
            SqlCommand cmd = null;
            string paidOrNot = "";
            con.Open();


            //Get the Value of the Login ID
            var user = Session["user"] as User;

            //Get the Session value of Paid and UnPaid
            Session["All"] = All;
            Session["Paid"] = new Budget { paid = Paid };
            Session["UnPaid"] = new Budget { unpaid = UnPaid };


            //Populates the New Budget Amount
            if(budgetAmount != null)
            {
                cmd = new SqlCommand("UPDATE Budget SET bAmount="+budgetAmount+" WHERE Id=" + user.Id + "", con);
                cmd.ExecuteNonQuery();
            }

            //Deletes the Item from the List
            if(Delete != null)
            {
                //I added the AND in so incase and Error comes up Here I know what I did
                cmd = new SqlCommand("DELETE FROM Budget WHERE itemID="+Delete+" AND Id="+user.Id+"", con);
                cmd.ExecuteNonQuery();

            }

            //Updates Whether the Item Has been Paid Or Not
            if(Update != null && UnCheck != null)
            {
                //I added the AND in so incase and Error comes up Here I know what I did
                cmd = new SqlCommand("SELECT * FROM Budget WHERE itemID="+Update+" AND Id="+user.Id+"", con);
                cmd.ExecuteNonQuery();

                SqlDataReader readBudgetInfo = cmd.ExecuteReader();

                while (readBudgetInfo.Read())
                {
                    paidOrNot = (string)readBudgetInfo[4];
                }

                readBudgetInfo.Close();

                if(paidOrNot == "Yes")
                {
                    paidOrNot = "No";
                }
                else
                {
                    paidOrNot = "Yes";
                }
                //I added the AND in so incase and Error comes up Here I know what I did
                cmd = new SqlCommand("UPDATE Budget SET Paid='"+paidOrNot+"' WHERE itemID=" +Update+ " AND Id="+user.Id+"", con);
                cmd.ExecuteNonQuery();
            }
            

            return View();
        }

	}
}