using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NguyenNhutDuy_2122110447.Context;
namespace NguyenNhutDuy_2122110447.Controllers
{
    public class CategoryController : Controller
    {
        ASPEntities2 db = new ASPEntities2();
        public ActionResult Index()
        {
            var dl = db.Categories.ToList();
            return View(dl);
        }
        public ActionResult ProductByCategory(int id, string viewMode = "grid")
        {
            var products = db.Products.Where(n => n.category_id == id).ToList();

            // Pass products and view mode to the view
            ViewBag.Products = products;
            ViewBag.ViewMode = viewMode;
            ViewBag.CategoryId = id;

            return View();
        }
    }
}