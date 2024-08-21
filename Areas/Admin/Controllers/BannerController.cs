using NguyenNhutDuy_2122110447.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace NguyenNhutDuy_2122110447.Areas.Admin.Controllers
{
    public class BannerController : Controller
    {
        // GET: Admin/Banner

        Entities3 db = new Entities3();
        public ActionResult Index(int? page)
        {
            int pageSize = 4; // Số sản phẩm trên mỗi trang
            int pageNumber = (page ?? 1); // Trang hiện tại, mặc định là trang 1

            var dl = db.Banners
                              .OrderBy(p => p.id) // Sắp xếp theo thuộc tính bạn muốn
                              .ToPagedList(pageNumber, pageSize);
            return View(dl);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Banner banner)
        {

            try
            {
                banner.created_at = DateTime.Now;
                db.Banners.Add(banner);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Index", "Banner");
        }

        public ActionResult Delete(int id)
        {
            try
            {
                Banner pd = db.Banners.Find(id);
                db.Banners.Remove(pd);
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
            var dl = db.Banners.Find(id);
            if (dl == null)
            {
                return HttpNotFound();
            }
            return View(dl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Banner banner)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dl = db.Banners.AsNoTracking().FirstOrDefault(b => b.id == banner.id);
                    if (dl != null)
                    {
                        banner.created_at = dl.created_at; // Giữ lại giá trị created_at gốc
                    }

                    banner.updated_at = DateTime.Now;
                    db.Entry(banner).State = EntityState.Modified;
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
            return View(banner);
        }
        public ActionResult Detail(int id)
        {
            var dl = db.Banners.Find(id);
            if (dl == null)
            {
                return HttpNotFound();
            }
            return View(dl);
        }

    }
}