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
    public class CountDownController : Controller
    {
        //
        // GET: /CountDown/
        public ActionResult CountDown()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CountDown(string year, string month, string day, string hour, string minute)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WeddingsForYouDB"].ConnectionString);

            con.Open();


            //Get the Session of the UserID
            var user = Session["user"] as User;

            if (ModelState.IsValid)
            {
                //Updates the Current Item Witht the New Information in the Database
                //I Think this is Wrong make an AND Statement to get the userID too
                SqlCommand cmd = new SqlCommand("UPDATE CountDown SET year='" + year + "', month='" + month + "', day='" + day + "', timeHour='"+hour+"', timeMinute='"+minute+"' WHERE Id=" + user.Id + "", con);
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