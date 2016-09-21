using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeddingsForYou.Models;
using System.Text;


namespace WeddingsForYou.Controllers
{
    public class DirectoryController : Controller
    {
        //
        // GET: /Directory/
        public ActionResult Directory()
        {
            return View();
        }

        public ActionResult CountryList()
        {
            IQueryable regions = Region.GetRegions();

            if (HttpContext.Request.IsAjaxRequest())
            {
                return Json(new SelectList(
                            regions,
                            "RegionCode",
                            "RegionName"), JsonRequestBehavior.AllowGet
                            );
            }
        
            return View(regions);
        }

        public ActionResult StateList(string CountryCode)
        {
            IQueryable categories = Category.GetCategories();//.Where(x => x.CountryCode == CountryCode);

            if (HttpContext.Request.IsAjaxRequest())
                return Json(new SelectList(
                                categories,
                                "CategoryID",
                                "CategoryName"), JsonRequestBehavior.AllowGet
                            );

            return View(categories);
        }

        [HttpPost]
        public ActionResult Directory(string Region, int Category)
        {

            Session["regionDetails"] = new Region { RegionCode = Region };
            Session["categoryDetails"] = new Category { CategoryID = Category };
          
            return View();
        }
	}
}