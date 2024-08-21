using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NguyenNhutDuy_2122110447.Context;
using NguyenNhutDuy_2122110447.Models;
using PagedList;
namespace NguyenNhutDuy_2122110447.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        Entities3 db = new Entities3();
        public ActionResult Index(int? page, string viewMode = "grid")
        {
            int pageSize = 4; // Số sản phẩm trên mỗi trang
            int pageNumber = (page ?? 1);

            var products = db.Products
                .OrderBy(p => p.id) // Sắp xếp theo thuộc tính bạn muốn
                .ToPagedList(pageNumber, pageSize);

            // Pass products and view mode to the view
            ViewBag.Products = products;
            ViewBag.ViewMode = viewMode;

            return View();
        }

        public ActionResult Detail(int id)
        {
            var pro = db.Products.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            ViewBag.Qty = pro.qty;
            return View(pro);
        }
    }
}