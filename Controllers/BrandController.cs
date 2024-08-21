using NguyenNhutDuy_2122110447.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NguyenNhutDuy_2122110447.Controllers
{
    public class BrandController : Controller
    {
        // GET: Brand
        Entities3 db = new Entities3();
        public ActionResult Index(int? page)
        {
            int pageSize = 5; // Số sản phẩm trên mỗi trang
            int pageNumber = (page ?? 1); // Trang hiện tại, mặc định là trang 1

            var dl = db.Brands
                              .OrderBy(p => p.id) // Sắp xếp theo thuộc tính bạn muốn
                              .ToPagedList(pageNumber, pageSize);
            return View(dl);
        }
        public ActionResult ProductByBrand(int? page, int id, string viewMode = "grid")
        {
            int pageSize = 4; // Số sản phẩm trên mỗi trang
            int pageNumber = (page ?? 1);

            var products = db.Products
                .Where(n => n.brand_id == id)
                .OrderBy(p => p.id) // Sắp xếp theo thuộc tính bạn muốn
                .ToPagedList(pageNumber, pageSize);

            // Pass products and view mode to the view
            ViewBag.Products = products;
            ViewBag.ViewMode = viewMode;
            ViewBag.CategoryId = id;

            return View();
        }
    }
}