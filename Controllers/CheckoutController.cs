using NguyenNhutDuy_2122110447.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using NguyenNhutDuy_2122110447.Context;
namespace NguyenNhutDuy_2122110447.Controllers
{
    public class CheckoutController : Controller
    {
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
        [HttpPost]
        public ActionResult Checkout(string phoneNumber, decimal grandTotal)
        {
            var user = db.Users.Find(Session["id"]);
            var listCart = (List<CartModel>)Session["cart"];
            Order order = new Order();
            order.user_id = user.id;
            order.phone = phoneNumber;
            order.dalivery_address = user.address;
            order.roral_amount = grandTotal;
            order.created_at = DateTime.Now;
            db.Orders.Add(order);
            db.SaveChanges();
            int idOrder = order.id;
            List<Order_Detail> list = new List<Order_Detail>();
            foreach (var item in listCart)
            {
                Order_Detail detail = new Order_Detail();
                detail.qty = item.Quantity;
                detail.order_id = idOrder;
                detail.product_id = item.Product.id;
                if (item.Product.price_sale == null || item.Product.price_sale == 0)
                {
                    detail.amount = item.Product.price * item.Quantity;
                }
                else
                {
                    detail.amount = item.Product.price_sale * item.Quantity;
                }

                detail.created_at = DateTime.Now;
                list.Add(detail);
                var product = db.Products.Find(item.Product.id);
                if (product != null)
                {
                    product.qty -= item.Quantity;
                }
            }
            db.Order_Detail.AddRange(list);
            db.SaveChanges();
            Session["cart"] = null;
            Session["count"] = 0;
            SendOrderConfirmationEmail(user.name, user.email, idOrder, grandTotal);
            TempData["Message"] = "Đặt hàng Thành công";
            return RedirectToAction("Index", "Home");
        }
        private void SendOrderConfirmationEmail(string name, string userEmail, int orderId, decimal grandTotal)
        {
            var fromAddress = new MailAddress("nguyennhutduy.cv@gmail.com", "Duy-Ot");
            var toAddress = new MailAddress(userEmail);
            const string subject = "Xác nhận đơn hàng";
            var body = new StringBuilder();
            body.AppendLine("Xin chào " + name);
            body.AppendLine();
            body.AppendLine($"Cảm ơn bạn đã đặt hàng tại cửa hàng của chúng tôi.");
            body.AppendLine($"Mã đơn hàng của bạn là: {orderId}");
            body.AppendLine($"Tổng số tiền: {grandTotal:N0} VND");
            body.AppendLine();
            body.AppendLine("Chúng tôi sẽ xử lý đơn hàng của bạn và thông báo khi có cập nhật.");
            body.AppendLine();
            body.AppendLine("Trân trọng,");
            body.AppendLine("Đội ngũ nhân viên Duy-Ot");

            using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.Credentials = new NetworkCredential("nguyennhutduy.cv@gmail.com", "agez ooej cqao zchd");
                smtpClient.EnableSsl = true;

                var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body.ToString(),
                    IsBodyHtml = false
                };

                smtpClient.Send(message);
            }
        }
    }
}