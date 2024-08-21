using NguyenNhutDuy_2122110447.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace NguyenNhutDuy_2122110447.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        // GET: Admin/Brand
        Entities3 db = new Entities3();
        public ActionResult Index(int? page)
        {
            int pageSize = 4; // Số sản phẩm trên mỗi trang
            int pageNumber = (page ?? 1); // Trang hiện tại, mặc định là trang 1

            var dl = db.Brands
                              .OrderBy(p => p.id) // Sắp xếp theo thuộc tính bạn muốn
                              .ToPagedList(pageNumber, pageSize);
            return View(dl);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Brand brand)
        {

            try
            {
                brand.created_at = DateTime.Now;
                db.Brands.Add(brand);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Index", "Brand");
        }
        public ActionResult Delete(int id)
        {
            try
            {
                Brand pd = db.Brands.Find(id);
                db.Brands.Remove(pd);
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
            var dl = db.Brands.Find(id);
            if (dl == null)
            {
                return HttpNotFound();
            }
            return View(dl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Brand brand)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dl = db.Brands.AsNoTracking().FirstOrDefault(b => b.id == brand.id);
                    if (dl != null)
                    {
                        brand.created_at = dl.created_at; // Giữ lại giá trị created_at gốc
                    }

                    brand.updated_at = DateTime.Now;
                    db.Entry(brand).State = EntityState.Modified;
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
            return View(brand);
        }
        public ActionResult Detail(int id)
        {
            var dl = db.Brands.Find(id);
            if (dl == null)
            {
                return HttpNotFound();
            }
            return View(dl);
        }
    }
}