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
    public class EditGuestItemController : Controller
    {
        //
        // GET: /EditGuestItem/
        public ActionResult EditGuestItem()
        {
            return View();
        }
        [HttpGet]
        public ActionResult EditGuestItem(int guestID)
        {
            Session["guestID"] = new Guests { GuestID = guestID };
            return View();
        }
        [HttpPost]
        public ActionResult EditGuestItem(string GuestName, string GuestEmail, string GuestCeremony, string GuestReception, string GuestHens, string GuestStag)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WeddingsForYouDB"].ConnectionString);

            con.Open();

            //Get the Session of the GuestID
            var guest = Session["guestID"] as Guests;

            //Get the Session of the UserID
            var user = Session["user"] as User;

            if (ModelState.IsValid)
            {
                //Updates the Current Item Witht the New Information in the Database
                SqlCommand cmd = new SqlCommand("UPDATE Guests SET guestName='" + GuestName + "', guestEmail='" + GuestEmail + "', Ceremony='" + GuestCeremony + "', Reception='"+GuestReception+"', Hens='"+GuestHens+"', Stag='"+GuestStag+"'  WHERE guestID=" + guest.GuestID + " AND Id="+user.Id+"", con);
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