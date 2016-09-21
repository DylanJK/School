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
    public class EditTaskItemController : Controller
    {
        //
        // GET: /EditTaskItem/
        public ActionResult EditTaskItem()
        {
            return View();
        }
        [HttpGet]
        public ActionResult EditTaskItem(int taskID)
        {
            //Get the Session so You can get the Item ID
            Session["editTaskItemId"] = new Tasks { TaskID = taskID };

            return View();
        }
        [HttpPost]
        public ActionResult EditTaskItem(string description, string complete)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WeddingsForYouDB"].ConnectionString);

            con.Open();

            //Get the Sessions of the Tasks
            var tasks = Session["editTaskItemId"] as Tasks;

            //Get the Session of the User
            var user = Session["user"] as User;

            if (ModelState.IsValid)
            {
                //Updates the Current Item Witht the New Information in the Database
                SqlCommand cmd = new SqlCommand("UPDATE Tasks SET description='" + description + "', Complete='" + complete + "' WHERE taskID=" + tasks.TaskID + " AND Id=" + user.Id + "", con);
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