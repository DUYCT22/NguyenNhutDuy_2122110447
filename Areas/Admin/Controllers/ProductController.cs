using NguyenNhutDuy_2122110447.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace NguyenNhutDuy_2122110447.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        ASPEntities2 db = new ASPEntities2();
        public ActionResult Index()
        {
            var dl= db.Products.ToList();
            return View(dl);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {

            try
            {
                db.Products.Add(product);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Index", "Product");
        }
        public ActionResult Delete(int id)
        {
            try
            {
                Product pd = db.Products.Find(id);
                db.Products.Remove(pd);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Index");
        }
    }
}