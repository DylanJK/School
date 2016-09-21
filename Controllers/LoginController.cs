using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WeddingsForYou.Models;

namespace WeddingsForYou.Controllers
{
    public class LoginController : Controller
    {

        string email = null;
        string passwordRead = null;
        //
        // GET: /Login/
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WeddingsForYouDB"].ConnectionString);

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(string name, string password, string emailAddress)
        {
            string userPassword = "";
            string userName = "";

            bool rowExists = false;

            con.Open();

            if (emailAddress != "")
            {
                if (ModelState.IsValid)
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM LoginInformation WHERE Email='" + emailAddress + "'", con);
                    cmd.ExecuteNonQuery();

                    SqlDataReader readLoginInformation = cmd.ExecuteReader();

                    while (readLoginInformation.Read())
                    {
                        rowExists = true;
                        userName = (string)readLoginInformation[2];
                        userPassword = (string)readLoginInformation[4];
                    }
                    if (rowExists == false)
                    {
                        ViewBag.Status = "Email Doesn't exist";

                    }
                    else
                    {
                        var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                        var message = new MailMessage();
                        message.To.Add(new MailAddress(emailAddress));  // replace with valid value 
                        message.From = new MailAddress("weddingsforeverinlove@hotmail.com");  // replace with valid value
                        message.Subject = "Password Reset for Weddings For You";
                        message.Body = string.Format(body, "weddingsforeverinlove@hotmail.com", "weddingsforeverinlove@hotmail.com", "Hello " + userName + " Your Password is " + userPassword + "");
                        message.IsBodyHtml = true;
                        ViewBag.Status = "Email Sent";
                        using (var smtp = new SmtpClient())
                        {
                            var credential = new NetworkCredential
                            {
                                UserName = "weddingsforeverinlove@hotmail.com",  // replace with valid value
                                Password = "weddingsAdmin"  // replace with valid value
                            };
                            smtp.Credentials = credential;
                            smtp.Host = "smtp-mail.outlook.com";
                            smtp.Port = 587;
                            smtp.EnableSsl = true;
                            await smtp.SendMailAsync(message);
                        }

                        ViewBag.Status = "Password Sent Successfully to the following email address";
                    }

              }
             
               

            }
            else if(ModelState.IsValid == false)
            {
                ViewBag.Status = "Email Doesn't exist";
            
            }

            else
            {
                int userId = 0;
                //Retrieves the Login Information

                SqlCommand cmd = new SqlCommand("SELECT Id, Name, Email, Password FROM LoginInformation WHERE Email='" + name + "' AND Password='" + password + "'", con);
                cmd.ExecuteNonQuery();

                SqlDataReader readLoginInformation = cmd.ExecuteReader();

                while (readLoginInformation.Read())
                {
                    userId = (int)readLoginInformation[0];
                    email = (string)readLoginInformation[2];
                    passwordRead = (string)readLoginInformation[3];

                    if (email.Contains(" "))
                    {
                        email = email.Substring(0, name.Length);
                    }
                    if (password.Contains(" "))
                    {
                        password = password.Substring(0, passwordRead.Length);
                    }

                    //Matches Login With Password
                    if (email.Equals(name) && password.Equals(password))
                    {
                        Session["user"] = new User { Id = userId, Login = name, Name = (string)readLoginInformation[1] };

                        return RedirectToAction("Index", "Home");
                    }

                }
                readLoginInformation.Close();


            }
            con.Close();

           return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            //Or Session["user"] = null
            return RedirectToAction("index", "Home");
        }
	}
}