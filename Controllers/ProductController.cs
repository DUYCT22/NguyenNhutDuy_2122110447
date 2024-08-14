using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NguyenNhutDuy_2122110447.Context;
namespace NguyenNhutDuy_2122110447.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(int id)
        {
            using (ASPEntities2 entities1 = new ASPEntities2())
            {
                var pro = entities1.Products.Find(id);
                if (pro == null)
                {
                    return HttpNotFound();
                }
                return View(pro);
            }
        }
    }
}