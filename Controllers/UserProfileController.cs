using WeddingsForYou.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;

namespace WeddingsForYou.Controllers
{
    public class UserProfileController : Controller
    {
        //
        // GET: /UserProfile/

        int idSub;
        int iPos;
        string userNameSub = null;
        string dNameSub = null;
        string emailSub = null;
        string passwordSub = null;

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WeddingsForYouDB"].ConnectionString);
         
        public ActionResult UserProfile()
        {
            var user = Session["user"] as User;
             if (user == null)
             {
                 return RedirectToAction("index", "Home");
             }
             else
             {
                 return View();
             }
        }
        [HttpPost]
        public ActionResult UserProfile(string password)
        {
                var user = Session["user"] as User;
                con.Open();
                SqlCommand cmdRead = new SqlCommand("SELECT * FROM LoginInformation WHERE Email='" + user.Login + "'", con);   
                cmdRead.ExecuteNonQuery();

                SqlDataReader readLoginInformation = cmdRead.ExecuteReader();

                while (readLoginInformation.Read())
                {
                    idSub = (int)readLoginInformation[0];
                    passwordSub = (string)readLoginInformation[4];

                    if (passwordSub.Contains(" "))
                    {
                        iPos = passwordSub.IndexOf(' ');
                        passwordSub = passwordSub.Substring(0, iPos);
                    }
                }
                
            readLoginInformation.Close();

            if (ModelState.IsValid)
            {

                SqlCommand cmd = new SqlCommand("UPDATE LoginInformation SET Password='"+password+"' WHERE Id='"+idSub+"'", con);              
                cmd.ExecuteNonQuery();
                con.Close();

                ViewBag.Status = "Update Successful";

            }
            else
            {
                ViewBag.Status = "Update Un-Successful Please Try Again";
            }


            return View();
        }
	}
}