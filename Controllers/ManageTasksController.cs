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
    public class ManageTasksController : Controller
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WeddingsForYouDB"].ConnectionString);

        //
        // GET: /ManageTasks/
        public ActionResult ManageTasks()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ManageTasks(string All, string complete, string inComplete, string Update, string UnCheck, string Delete)
        {
            string taskComplete = "";
            SqlCommand cmd = null;
            con.Open();

            //Get the Sessions of the User
             var user = Session["user"] as User;

            //Get the Session value of Paid and UnPaid
            Session["All"] = All;
            Session["Complete"] = new Tasks { Complete = complete };
            Session["InComplete"] = new Tasks { InComplete = inComplete };


            //Deletes the Item from the List
            if (Delete != null)
            {
                //I added the AND in so incase and Error comes up Here I know what I did
                cmd = new SqlCommand("DELETE FROM Tasks WHERE taskID=" + Delete + " AND Id=" + user.Id + "", con);
                cmd.ExecuteNonQuery();

            }


            //Updates Whether the Task has been Complete or Not
            if (Update != null && UnCheck != null)
            {
                cmd = new SqlCommand("SELECT * FROM Tasks WHERE taskID=" + Update + " AND Id="+user.Id+"", con);
                cmd.ExecuteNonQuery();

                SqlDataReader readTasksInfo = cmd.ExecuteReader();

                while (readTasksInfo.Read())
                {
                    taskComplete = (string)readTasksInfo[3];
                }

                readTasksInfo.Close();

                //If the Item is Complete(Yes)
                if (taskComplete.Contains("Yes"))
                {
                    taskComplete = "No";
                }
                //If the Item is InComplete(No)
                else
                {
                    taskComplete = "Yes";
                }

                cmd = new SqlCommand("UPDATE Tasks SET Complete='" + taskComplete + "' WHERE taskID=" + Update + " AND Id=" + user.Id + "", con);
                cmd.ExecuteNonQuery();
            }


            return View();
        }
	}
}