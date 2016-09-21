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
    public class TaskItemController : Controller
    {
        //
        // GET: /TaskItem/
        public ActionResult TaskItem()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TaskItem(string description, string Complete)
        {

            int itemIDinDatabase = 0;

            //Get the Session of the User so we can get the ID
            var user = Session["user"] as User;

            if (ModelState.IsValid)
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WeddingsForYouDB"].ConnectionString);

                con.Open();


                //Read From the Database so I can get the Value of the User ID
                SqlCommand readitemIDCMD = new SqlCommand("SELECT * FROM Tasks WHERE Id=" + user.Id + "", con);
                readitemIDCMD.ExecuteNonQuery();

                SqlDataReader readTaskInfo = readitemIDCMD.ExecuteReader();

                while (readTaskInfo.Read())
                {
                    itemIDinDatabase++;
                }

                readTaskInfo.Close();

                itemIDinDatabase = itemIDinDatabase + 1;

                SqlCommand cmd = new SqlCommand("Insert into Tasks values ('" + user.Id + "','" + itemIDinDatabase + "','" + description + "','" + Complete + "')", con);
                cmd.ExecuteNonQuery();

                ViewBag.Status = "Task Submitted Successful";
            }
            else
            {
                ViewBag.Status = "Task Un-Successfully Submitted";
            }

            return View();
        }
	}
}