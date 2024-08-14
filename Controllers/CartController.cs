using NguyenNhutDuy_2122110447.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NguyenNhutDuy_2122110447.Models;
namespace NguyenNhutDuy_2122110447.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        ASPEntities2 db = new ASPEntities2();
        public ActionResult Index()
        {
            List<CartModel> cart = Session["cart"] as List<CartModel>;

            if (cart != null)
            {
                var total = cart.Sum(item => item.Product.price_sale * item.Quantity);
                ViewBag.Total = total;
            }
            else
            {
                ViewBag.Total = 0;
            }

            return View(cart);
        }
        public ActionResult AddToCart(int id, int quantity)
        {
            if (Session["email"] == null)
            {
                return Json(new { redirectToLogin = true }, JsonRequestBehavior.AllowGet);
            }

            if (Session["cart"] == null)
                {
                    List<CartModel> cart = new List<CartModel>();
                    cart.Add(new CartModel { Product = db.Products.Find(id), Quantity = quantity });
                    Session["cart"] = cart;
                    Session["count"] = 1;
                }
                else
                {
                    List<CartModel> cart = (List<CartModel>)Session["cart"];
                    //kiểm tra sản phẩm có tồn tại trong giỏ hàng chưa???
                    int index = isExist(id);
                    if (index != -1)
                    {
                        //nếu sp tồn tại trong giỏ hàng thì cộng thêm số lượng
                        cart[index].Quantity += quantity;
                    }
                    else
                    {
                        //nếu không tồn tại thì thêm sản phẩm vào giỏ hàng
                        cart.Add(new CartModel { Product = db.Products.Find(id), Quantity = quantity });
                        //Tính lại số sản phẩm trong giỏ hàng
                        Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                    }
                    Session["cart"] = cart;
                }
                return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
            
        }
        private int isExist(int id)
        {
            List<CartModel> cart = (List<CartModel>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.id.Equals(id))
                    return i;
            return -1;
        }
        //xóa sản phẩm khỏi giỏ hàng theo id

        public ActionResult Remove(int Id)
        {
            List<CartModel> li = (List<CartModel>)Session["cart"];
            li.RemoveAll(x => x.Product.id == Id);
            Session["cart"] = li;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }

    }
}