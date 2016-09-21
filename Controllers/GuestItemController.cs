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
    public class GuestItemController : Controller
    {
        //
        // GET: /GuestItem/
        public ActionResult GuestItem()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GuestItem(int id)
        {
            Session["userID"] = new User { Id = id };
            return View();
        }
        [HttpPost]
        public ActionResult GuestItem(string GuestName, string GuestEmail, string GuestCeremony, string GuestReception, string GuestHens, string GuestStag)
        {

            int guestIDinDatabase = 0;

            if (ModelState.IsValid)
            {
                var user = Session["userID"] as User;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WeddingsForYouDB"].ConnectionString);

                con.Open();


                //Read From the Database so I can get the Value of the BudgetAmount
                SqlCommand readGuestCMD = new SqlCommand("SELECT * FROM Guests WHERE Id=" + user.Id + "", con);
                readGuestCMD.ExecuteNonQuery();

                SqlDataReader readGuestInfo = readGuestCMD.ExecuteReader();

                while (readGuestInfo.Read())
                {
                    guestIDinDatabase++;
                }

                readGuestInfo.Close();

                guestIDinDatabase = guestIDinDatabase + 1;

                SqlCommand cmd = new SqlCommand("Insert into Guests values ('" + user.Id + "','" + guestIDinDatabase + "','" + GuestName + "','" + GuestEmail + "','" + GuestCeremony + "','" + GuestReception + "','" + GuestHens + "','" + GuestStag + "')", con);
                cmd.ExecuteNonQuery();

                ViewBag.Status = "Guest Submitted Successful";

            }
            else
            {
                ViewBag.Status = "Guest Un-Successfully Submitted";
            }

            return View();
        }
	}
}