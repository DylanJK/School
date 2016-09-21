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
    public class ForumController : Controller
    {
        //
        // GET: /Forum/
          SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WeddingsForYouDB"].ConnectionString);

        public ActionResult Forum()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Forum(ForumTopic forumTopic)
        {
            return View();
        }
	}
}