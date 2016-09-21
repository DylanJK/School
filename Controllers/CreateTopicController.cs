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
    public class CreateTopicController : Controller
    {
        //
        // GET: /CreateTopic/
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WeddingsForYouDB"].ConnectionString);

        public ActionResult CreateTopic()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTopic(Forum forum)
        {
            // Insert Details of Forum into Database

            if (ModelState.IsValid)
            {
                con.Open();

                int nextID = 0;

                SqlCommand readCMD = new SqlCommand("SELECT * FROM ForumInfo", con);
                readCMD.ExecuteNonQuery();

                SqlDataReader readForumLength = readCMD.ExecuteReader();
                while (readForumLength.Read())
                {
                    nextID++;
                }

                nextID = nextID + 1;

                readForumLength.Close();

                SqlCommand cmd = new SqlCommand("Insert into ForumInfo values ('" +nextID + "','" + forum.bloggerName + "','" + forum.title + "','" + forum.subHeading + "')", con);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("Insert into ForumTopic values ('"+nextID+"','"+forum.bloggerName+"','"+forum.subHeading+"')",con);
                cmd.ExecuteNonQuery();
                con.Close();

                ViewBag.Status = "Topic Submitted Successful";

            }
            else
            {
                ViewBag.Status = "Submission Un-Successful Please Try Again";
            }



            return View();
        }
	}
}