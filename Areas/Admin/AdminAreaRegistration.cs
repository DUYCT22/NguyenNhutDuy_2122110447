using System.Web.Mvc;

namespace NguyenNhutDuy_2122110447.Areas.Admin
{
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