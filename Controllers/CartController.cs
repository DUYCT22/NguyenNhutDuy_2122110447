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
        Entities3 db = new Entities3();
        public ActionResult Index()
        {
            List<CartModel> cart = Session["cart"] as List<CartModel>;

            if (cart != null)
            {
                foreach (var item in cart)
                {
                    var product = db.Products.FirstOrDefault(p => p.id == item.Product.id);
                    if (product != null)
                    {
                        item.AvailableQuantity = product.qty.GetValueOrDefault(); // Lưu số lượng có sẵn vào CartModel
                    }
                }
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

            // Lấy thông tin sản phẩm từ cơ sở dữ liệu
            var product = db.Products.FirstOrDefault(p => p.id == id);
            if (product == null)
            {
                return Json(new { Message = "Sản phẩm không tồn tại" }, JsonRequestBehavior.AllowGet);
            }

            int availableQuantity = product.qty.GetValueOrDefault(); // Số lượng có sẵn trong kho

            if (Session["cart"] == null)
            {
                // Nếu giỏ hàng chưa tồn tại, tạo mới và thêm sản phẩm vào giỏ hàng
                List<CartModel> cart = new List<CartModel>();
                if (quantity > availableQuantity)
                {
                    quantity = availableQuantity; // Giới hạn số lượng không vượt quá số lượng có sẵn
                }
                cart.Add(new CartModel { Product = product, Quantity = quantity });
                Session["cart"] = cart;
                Session["count"] = 1;
            }
            else
            {
                // Nếu giỏ hàng đã tồn tại, kiểm tra và cập nhật sản phẩm
                List<CartModel> cart = (List<CartModel>)Session["cart"];
                int index = isExist(id);

                if (index != -1)
                {
                    // Nếu sản phẩm đã tồn tại trong giỏ hàng, kiểm tra số lượng có hợp lệ không
                    var existingProduct = cart[index];
                    int newQuantity = existingProduct.Quantity + quantity;

                    if (newQuantity > availableQuantity)
                    {
                        newQuantity = availableQuantity; // Giới hạn số lượng không vượt quá số lượng có sẵn
                    }

                    existingProduct.Quantity = newQuantity;
                }
                else
                {
                    // Nếu sản phẩm chưa tồn tại trong giỏ hàng, thêm sản phẩm mới
                    if (quantity > availableQuantity)
                    {
                        quantity = availableQuantity; // Giới hạn số lượng không vượt quá số lượng có sẵn
                    }
                    cart.Add(new CartModel { Product = product, Quantity = quantity });
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                }

                Session["cart"] = cart;
            }

            return Json(new { Message = "Thành công" }, JsonRequestBehavior.AllowGet);
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