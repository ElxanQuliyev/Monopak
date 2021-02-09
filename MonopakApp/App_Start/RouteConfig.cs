using MonopakApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using static MonopakApp.Helpers.SeoFriendlyRoute;

namespace MonopakApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MonopakApp.Controllers" }
            );
            routes.MapRoute(
               name: "SearchProducts",
               url: "search/{category}",
               defaults: new { area = "", controller = "Product", action = "Search", category = UrlParameter.Optional },
               namespaces: new[] { "MonopakApp.Controllers" }
           );
            routes.Add("ProductDetails", new SeoFriendlyRoute("mehsullar/{id}",
            new RouteValueDictionary(new { controller = "Product", action = "ProductList" }),
            new MvcRouteHandler()));
            routes.MapRoute(
                name: "About",
                url: "haqqimizda",
                defaults: new { controller = "Home", action = "About", id = UrlParameter.Optional },
                namespaces: new[] { "MonopakApp.Controllers" }
            );
            routes.MapRoute(
              name: "productList",
              url: "mehsullar",
              defaults: new { controller = "Product", action = "ProductList", id = UrlParameter.Optional },
              namespaces: new[] { "MonopakApp.Controllers" }
          );
            routes.MapRoute(
                name: "Contact",
                url: "elaqe",
                defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional },
                namespaces: new[] { "MonopakApp.Controllers" }
            );

            routes.MapRoute(
                name: "Login",
                url: "daxil-ol",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "MonopakApp.Controllers" }
            );
            routes.MapRoute(
                name: "checkout",
                url: "checkout",
                defaults: new { controller = "Orders", action = "Checkout", id = UrlParameter.Optional },
                namespaces: new[] { "MonopakApp.Controllers" }
            );
            routes.MapRoute(
                name: "OrderCart",
                url: "kart",
                defaults: new { controller = "Orders", action = "Cart", id = UrlParameter.Optional },
                namespaces: new[] { "MonopakApp.Controllers" }
            );
            routes.MapRoute(
                name: "Qeydiyyat",
                url: "qeydiyyat",
                defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional },
                namespaces: new[] { "MonopakApp.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "MonopakApp.Controllers" }

                );
            

        }
    }
}
