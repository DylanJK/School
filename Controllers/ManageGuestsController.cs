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
    public class ManageGuestsController : Controller
    {
        //
        // GET: /ManageGuests/
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WeddingsForYouDB"].ConnectionString);

        List<string> guestNames = new List<string>();
        List<string> guestEmails = new List<string>();

        string guestName = "";
        string guestEmail = "";

  
        public ActionResult ManageGuests()
        {

            return View();
        }
        [HttpPost]
        public async Task<ActionResult>  ManageGuests(string Delete, string SendInvitations)
        {

            SqlCommand cmd = null;
            con.Open();


            //Get the Value of the Login ID
            var user = Session["user"] as User;

            cmd = new SqlCommand("SELECT * FROM Guests WHERE Id=" + user.Id + "", con);

            cmd.ExecuteNonQuery();

            SqlDataReader readGuestInfo = cmd.ExecuteReader();

            while (readGuestInfo.Read())
            {
                guestName = (string)readGuestInfo[2];
                guestEmail = (string)readGuestInfo[3];

                guestNames.Add(guestName);
                guestEmails.Add(guestEmail);
            }

            //Deletes the Item from the List
            if (Delete != null)
            {
                //I added the AND in so incase and Error comes up Here I know what I did
                cmd = new SqlCommand("DELETE FROM Guests WHERE guestID=" + Delete + " AND Id=" + user.Id + "", con);
                cmd.ExecuteNonQuery();

            }
            if (SendInvitations != null)
            {

                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                for (int i = 0; i < guestNames.Count; i++)
                {
                    message.To.Add(new MailAddress(guestEmails[i].ToString()));  // replace with valid value
                }
                message.From = new MailAddress("weddingsforeverinlove@hotmail.com");  // replace with valid value
                message.Subject = "Invite To Wedding";

                message.Body = string.Format(body, "Wedding Admin", "weddingsforeverinlove@hotmail.com", "Hello \n You have been invited to Attend James and Jackies Wedding");
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
                    //return RedirectToAction("Contact");
                }
            }
            return View();
        }
	}
}