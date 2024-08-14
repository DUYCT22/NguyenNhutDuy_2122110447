using NguyenNhutDuy_2122110447.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NguyenNhutDuy_2122110447.Context;
namespace NguyenNhutDuy_2122110447.Controllers
{
    public class HomeController : Controller
    {
        ASPEntities2 db = new ASPEntities2();
        public ActionResult Index()
        {
            Home dl = new Home();
            dl.ListCategory = db.Categories.ToList();
            dl.ListProduct = db.Products.ToList();
            return View(dl);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Category()
        {
            return View();
        }
        public ActionResult ListGrip()
        {
            return View();
        }
        public ActionResult ListingList()
        {
            return View();
        }
        public ActionResult Info()
        {
            return View();
        }
    }
}