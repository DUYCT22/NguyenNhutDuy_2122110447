using NguyenNhutDuy_2122110447.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace NguyenNhutDuy_2122110447.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        Entities3 db = new Entities3();
        public ActionResult Index(int? page)
        {
            int pageSize = 4; // Số sản phẩm trên mỗi trang
            int pageNumber = (page ?? 1); // Trang hiện tại, mặc định là trang 1

            var dl = db.Categories
                              .OrderBy(p => p.id) // Sắp xếp theo thuộc tính bạn muốn
                              .ToPagedList(pageNumber, pageSize);
            return View(dl);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {

            try
            {
                category.created_at = DateTime.Now;
                db.Categories.Add(category);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Index", "Category");
        }
        public ActionResult Delete(int id)
        {
            try
            {
                Category pd = db.Categories.Find(id);
                db.Categories.Remove(pd);
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
            var dl = db.Categories.Find(id);
            if (dl == null)
            {
                return HttpNotFound();
            }
            return View(dl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dl = db.Categories.AsNoTracking().FirstOrDefault(b => b.id == category.id);
                    if (dl != null)
                    {
                        category.created_at = dl.created_at; // Giữ lại giá trị created_at gốc
                    }

                    category.updated_at = DateTime.Now;
                    db.Entry(category).State = EntityState.Modified;
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
            return View(category);
        }
        public ActionResult Detail(int id)
        {
            var dl = db.Categories.Find(id);
            if (dl == null)
            {
                return HttpNotFound();
            }
            return View(dl);
        }
    }
}