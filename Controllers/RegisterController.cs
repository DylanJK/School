using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeddingsForYou.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;



namespace WeddingsForYou.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Register/
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WeddingsForYouDB"].ConnectionString);

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(Register register)
        {

            int userType = 0;

            int nextID = 0;

            // Insert user into Database

            if (ModelState.IsValid)
            {
                con.Open();

                //Rea the Database to Find Out the Next ID which should be used
                SqlCommand readCMD = new SqlCommand("SELECT * FROM LoginInformation", con);
                readCMD.ExecuteNonQuery();

                SqlDataReader readLoginLength = readCMD.ExecuteReader();
                while (readLoginLength.Read())
                {
                    nextID++;
                }

                nextID = nextID + 1;
                readLoginLength.Close();

                if(register.UserType == "Customer")
                {
                    userType = 1;
                }
                if (register.UserType == "Client")
                {
                    userType = 2;
                }

                SqlCommand cmd = new SqlCommand("Insert into LoginInformation values ('"+nextID+"','"+userType+"','"+ register.Login+"','"+ register.Name+"','"+register.Email+"','"+register.Password+"')", con);
                cmd.ExecuteNonQuery();

                //Write into the Budget So You store the Users ID and The Initial Item ID
                cmd = new SqlCommand("Insert into Budget values ('"+ nextID +"','1','','','','')", con);
                cmd.ExecuteNonQuery();

                //Write into the Tasks Table So You store the Users ID and The Initial Item ID
                cmd = new SqlCommand("Insert into Tasks values ('" + nextID + "','1','','')", con);
                cmd.ExecuteNonQuery();


                //Write into the Guests Table So You store the Users ID and The Initial Guest ID
                cmd = new SqlCommand("Insert into Guests values ('" + nextID + "','1','','','','','','')", con);
                cmd.ExecuteNonQuery();

                //Write into the WeddingTables Table So You store the Users ID and The Initial table ID
                cmd = new SqlCommand("Insert into WeddingTables values ('" + nextID + "','1','','')", con);
                cmd.ExecuteNonQuery();

                //Write into the WeddingLocation Table So You store the Users ID and The Initial table ID
                cmd = new SqlCommand("Insert into WeddingLocation values ('" + nextID + "','','','')", con);
                cmd.ExecuteNonQuery();

                //Write into the CountDown Table So You store the Users ID and The Initial table ID
                cmd = new SqlCommand("Insert into CountDown values ('" + nextID + "','','','','','')", con);
                cmd.ExecuteNonQuery();


                  var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress(register.Email));  // replace with valid value 
                message.From = new MailAddress("weddingsforeverinlove@htmail.com");  // replace with valid value
                message.Subject = "WeddingsForYou Registration";
                message.Body = string.Format(body, "weddingsforeverinlove@hotmail.com", "weddingsforeverinlove@hotmail.com", "Hello "+register.Name+" thank you for registering with weddings for you your login name is "+register.Email+" and your password is "+register.Password+"");
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

                con.Close();


                ViewBag.Status = "Registration Successful";

            }
            else
            {
                ViewBag.Status = "Registration Un-Successful Please Try Again";
            }


            return View();
        }
    }
}