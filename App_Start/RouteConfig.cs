using NguyenNhutDuy_2122110447.Areas.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
namespace NguyenNhutDuy_2122110447
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
            "Default",
            "{controller}/{action}/{id}",
            new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            new[] { "NguyenNhutDuy_2122110447.Controllers" }
            );
            routes.MapRoute(
                name: "Product",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ProductDetail",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Cart",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Category",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Category", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ListGrip",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "ListGrip", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ListingList",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "ListingList", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Info",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Info", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Login",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Register",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ProductByCategory",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Category", action = "ProductByCategory", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "Checkout",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Checkout", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "Order",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Order", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "OrderDetail",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Order", action = "Details", id = UrlParameter.Optional }
          );
            routes.MapRoute(
            name: "ProductByBrand",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Brand", action = "ProductByBrand", id = UrlParameter.Optional }
        );
        }
    }
}
