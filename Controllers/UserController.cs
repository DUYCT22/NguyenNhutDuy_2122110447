using Microsoft.Ajax.Utilities;
using NguyenNhutDuy_2122110447.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NguyenNhutDuy_2122110447.Controllers
{
    public class UserController : Controller
    {
        Entities3 asp = new Entities3();
        // GET: User
        public ActionResult Register()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Register(User objuser)
        {
            try
            {
                objuser.password = CreateMD5(objuser.password);
                objuser.created_at = DateTime.Now;
                objuser.role = 0;
                asp.Users.Add(objuser);
                asp.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw;
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User objuser)
        {
            objuser.password = CreateMD5(objuser.password);
            var Flag = asp.Users.Where(n=>n.email == objuser.email && n.password == objuser.password).ToList();
            if (Flag.Count > 0) {
                Session["email"] = Flag.FirstOrDefault().email.ToString();
                Session["id"] = Flag.FirstOrDefault().id;
            }
            return RedirectToAction("Index", "Home");
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
        public ActionResult Profile(int id)
        {
            User dl = asp.Users.Find(id);
            dl.ToString();
            return View();
        }
    }
}