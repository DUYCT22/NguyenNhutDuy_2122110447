using NguyenNhutDuy_2122110447.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NguyenNhutDuy_2122110447.Models;
namespace NguyenNhutDuy_2122110447.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        Entities3 db = new Entities3();
        public ActionResult Index()
        {
            if (Session["id"] == null)
            {
                Url.Action("Login", "User");
            }
            var userId = (int)Session["id"];
            var dl = db.Orders.Where(n => n.user_id == userId).ToList();
            return View(dl);
        }
        public ActionResult Details(int id)
        {
            if (Session["id"] == null)
            {
                Url.Action("Login", "User");
            }
            var dl = db.Order_Detail.Where(n=>n.order_id == id).ToList();
            return View(dl);
        }
    }
}