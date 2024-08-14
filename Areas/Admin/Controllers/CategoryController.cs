using NguyenNhutDuy_2122110447.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NguyenNhutDuy_2122110447.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        ASPEntities2 db = new ASPEntities2();
        public ActionResult Index()
        {
            var dl = db.Categories.ToList();
            return View(dl);
        }
    }
}