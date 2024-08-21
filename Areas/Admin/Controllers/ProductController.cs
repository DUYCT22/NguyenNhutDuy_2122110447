using NguyenNhutDuy_2122110447.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using PagedList;
namespace NguyenNhutDuy_2122110447.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        Entities3 db = new Entities3();
        public ActionResult Index(int? page)
        {
            int pageSize = 4; // Số sản phẩm trên mỗi trang
            int pageNumber = (page ?? 1); // Trang hiện tại, mặc định là trang 1

            var dl = db.Products
                              .OrderBy(p => p.id) // Sắp xếp theo thuộc tính bạn muốn
                              .ToPagedList(pageNumber, pageSize);
            return View(dl);
        }

        public ActionResult Create()
        {
            var cate = new SelectList(db.Categories.ToList(), "id", "name");
            var bra = new SelectList(db.Brands.ToList(), "id", "name");
            ViewBag.Categories = cate;
            ViewBag.Brands = bra;
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {

            try
            {
                product.created_at = DateTime.Now;
                db.Products.Add(product);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Thêm sản phẩm thành công!";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi thêm sản phẩm!";
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
        public ActionResult Edit(int id)
        {
            var dl = db.Products.Find(id);
            if (dl == null)
            {
                return HttpNotFound();
            }

            // Tạo SelectList cho dropdown
            ViewBag.brand_id = new SelectList(db.Brands, "id", "name", dl.brand_id);
            ViewBag.category_id = new SelectList(db.Categories, "id", "name", dl.category_id);

            return View(dl);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dl = db.Products.AsNoTracking().FirstOrDefault(b => b.id == product.id);
                    if (dl != null)
                    {
                        product.created_at = dl.created_at; // Giữ lại giá trị created_at gốc
                    }

                    product.updated_at = DateTime.Now;
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Cập nhật thành công!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Log the error
                    System.Diagnostics.Debug.WriteLine($"Lỗi: {ex.Message}");
                    TempData["ErrorMessage"] = "Có lỗi xảy ra trong quá trình cập nhật.";
                }
            }
            return View(product);
        }
        public ActionResult Detail(int id)
        {
            var dl = db.Products.Find(id);
            if (dl == null)
            {
                return HttpNotFound();
            }
            return View(dl);
        }
    }
}