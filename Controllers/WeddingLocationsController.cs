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
    public class WeddingLocationsController : Controller
    {
        //
        // GET: /WeddingLocations/
        public ActionResult WeddingLocations()
        {
            return View();
        }
        [HttpPost]
        public ActionResult WeddingLocations(string CeremonyLocation, string ReceptionLocation, string HoneymoonLocation)
        {


            if (ModelState.IsValid)
            {
                //Get the Session of the UserID
                var user = Session["user"] as User;

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WeddingsForYouDB"].ConnectionString);

                con.Open();


                SqlCommand cmd = new SqlCommand("Insert into WeddingLocation values ('" + user.Id+ "','" + CeremonyLocation + "','"+ReceptionLocation+"','"+HoneymoonLocation+"')", con);
                cmd.ExecuteNonQuery();

                ViewBag.Status = "Data Submitted Successful";
            }
            else
            {
                ViewBag.Status = "Data Un-Successfully Submitted";
            }

            return View();
        }
	}
}