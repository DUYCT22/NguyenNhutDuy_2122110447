using System.Web;
using System.Web.Mvc;

namespace NguyenNhutDuy_2122110447.Areas.Admin
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var userId = httpContext.Session["id"] as int?;
            return userId != null && userId == 4; // Thay đổi điều kiện theo nhu cầu của bạn
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Chuyển hướng người dùng đến trang đăng nhập nếu không được phép truy cập
            if (filterContext.HttpContext.Session["id"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(
                        new { controller = "User", action = "Login", area = "" }
                    )
                );
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(
                        new { controller = "User", action = "Login", area = "Admin" }
                    )
                );
            }
        }
    }   
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute("Admin_default","Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "NguyenNhutDuy_2122110447.Areas.Admin.Controllers" }
            );
            context.MapRoute("Admin_Product", "Admin/{controller}/{action}/{id}",
               new { action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "NguyenNhutDuy_2122110447.Areas.Admin.Controllers" }
           );
            context.MapRoute("Admin_Product_Create", "Admin/{controller}/{action}/{id}",
               new { action = "Create", id = UrlParameter.Optional },
               namespaces: new[] { "NguyenNhutDuy_2122110447.Areas.Admin.Controllers" }
           );
            context.MapRoute("Admin_Product_Delete", "Admin/{controller}/{action}/{id}",
               new { action = "Delete", id = UrlParameter.Optional },
               namespaces: new[] { "NguyenNhutDuy_2122110447.Areas.Admin.Controllers" }
           );


            context.MapRoute("Admin_Cateory", "Admin/{controller}/{action}/{id}",
               new { action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "NguyenNhutDuy_2122110447.Areas.Admin.Controllers" }
           );
        }
    }
}