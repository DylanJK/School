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
    public class ForumTopicController : Controller
    {
        //
        // GET: /ForumTopic/
        public ActionResult ForumTopic()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ForumTopic(ForumTopic forumTopic, int id)
        {            
            //Query the ID match and produce all in Descending order ordering by ID

            Session["topicID"] = new ForumTopic { ID = id };
           
            return View();
        }
        [HttpPost]
        public ActionResult ForumTopic(int id, string bloggerPostName, string description)
        { //id, bloggerPostName and description all the values that get passed through 
          // have to identically match the variables in the ForumTopic class
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WeddingsForYouDB"].ConnectionString);

            con.Open();

            SqlCommand cmd = new SqlCommand("Insert into ForumTopic values ('" + id + "','" + bloggerPostName + "','" + description + "')", con);
            cmd.ExecuteNonQuery();

            return View();
        }
	}
}