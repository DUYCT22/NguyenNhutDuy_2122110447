using NguyenNhutDuy_2122110447.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NguyenNhutDuy_2122110447.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        Entities3 db = new Entities3();
        public ActionResult Index(int? page)
        {
            int pageSize = 4; // Số sản phẩm trên mỗi trang
            int pageNumber = (page ?? 1); // Trang hiện tại, mặc định là trang 1

            var dl = db.Users
                              .OrderBy(p => p.id) // Sắp xếp theo thuộc tính bạn muốn
                              .ToPagedList(pageNumber, pageSize);
            return View(dl);
        }
        public ActionResult Delete(int id)
        {
            try
            {
                User pd = db.Users.Find(id);
                db.Users.Remove(pd);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Index");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User objuser)
        {
            // Mã hóa mật khẩu
            objuser.password = CreateMD5(objuser.password);

            // Tìm người dùng trong cơ sở dữ liệu với email và mật khẩu đã mã hóa
            var user = db.Users
                         .Where(n => n.email == objuser.email && n.password == objuser.password)
                         .FirstOrDefault();

            if (user != null)
            {
                if (user.role == 1)
                {
                    Session["email"] = user.email;
                    Session["id"] = user.id;
                    Session["role"] = user.role;
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else
                {
                    Session["email"] = user.email;
                    Session["id"] = user.id;
                    Session["role"] = user.role;

                    return RedirectToAction("Index", "Home");
                }
            }

            // Nếu đăng nhập không thành công, quay lại trang đăng nhập với thông báo lỗi
            ModelState.AddModelError("", "Đăng nhập không thành công. Vui lòng kiểm tra lại email hoặc mật khẩu.");
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                //return Convert.ToString(hashBytes); // .NET 5 +

                //Convert the byte array to hexadecimal string prior to.NET 5
                StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        public ActionResult Detail(int id)
        {
            var dl = db.Users.Find(id);
            if (dl == null)
            {
                return HttpNotFound();
            }
            return View(dl);
        }
    }
}