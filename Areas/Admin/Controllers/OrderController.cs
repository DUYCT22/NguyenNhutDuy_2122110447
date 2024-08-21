using NguyenNhutDuy_2122110447.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NguyenNhutDuy_2122110447.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Admin/Order
        Entities3 db = new Entities3();
        public ActionResult Index(int? page)
        {
            int pageSize = 4; // Số sản phẩm trên mỗi trang
            int pageNumber = (page ?? 1); // Trang hiện tại, mặc định là trang 1

            var dl = db.Orders
                              .OrderBy(p => p.id) // Sắp xếp theo thuộc tính bạn muốn
                              .ToPagedList(pageNumber, pageSize);
            return View(dl);
        }
        public ActionResult Order_Detail(int? page)
        {
            int pageSize = 4; // Số sản phẩm trên mỗi trang
            int pageNumber = (page ?? 1); // Trang hiện tại, mặc định là trang 1

            var dl = db.Order_Detail
                              .OrderBy(p => p.id) // Sắp xếp theo thuộc tính bạn muốn
                              .ToPagedList(pageNumber, pageSize);
            return View(dl);
        }
        public ActionResult Delete(int id)
        {
            try
            {
                Order order = db.Orders.Find(id);
                if (order != null)
                {
                    var orderDetails = db.Order_Detail.Where(od => od.order_id == id).ToList();
                    foreach (var item in orderDetails)
                    {
                        db.Order_Detail.Remove(item);
                    }
                    db.Orders.Remove(order);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Index");
        }
    }
}