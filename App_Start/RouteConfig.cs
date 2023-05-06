using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DuLichV2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "CheckOut",
                url: "thanh-toan",
                defaults: new { controller = "BookingCart", action = "CheckOut", id = UrlParameter.Optional },
                namespaces: new[] { "DuLichV2.Controllers" }
            );
            routes.MapRoute(
                name: "BookingCart",
                url: "gio-hang",
                defaults: new { controller = "BookingCart", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "DuLichV2.Controllers" }
            );
            routes.MapRoute(
                name: "CategoryTour",
                url: "danh-muc-tour/{Alias}-{Id}",
                defaults: new { controller = "Tours", action = "TourCategory", id = UrlParameter.Optional },
                namespaces: new[] { "DuLichV2.Controllers" }
            );
            routes.MapRoute(
                name: "Tour",
                url: "tour",
                defaults: new { controller = "Tours", action = "Index", Alias = UrlParameter.Optional },
                namespaces: new[] { "DuLichV2.Controllers" }
            );
            routes.MapRoute(
                name: "DetailTour",
                url: "chi-tiet/{Alias}-t{Id}",
                defaults: new { controller = "Tours", action = "Detail", Alias = UrlParameter.Optional },
                namespaces: new[] { "DuLichV2.Controllers" }
            );
            routes.MapRoute(
                name: "NewsList",
                url: "tin-tuc",
                defaults: new { controller = "News", action = "Index", Alias = UrlParameter.Optional },
                namespaces: new[] { "DuLichV2.Controllers" }
            );
            routes.MapRoute(
                name: "DetailNews",
                url: "chi-tiet-tin/{Alias}-n{Id}",
                defaults: new { controller = "News", action = "Detail", Alias = UrlParameter.Optional },
                namespaces: new[] { "DuLichV2.Controllers" }
            );
            routes.MapRoute(
                name: "AboutUs",
                url: "gioi-thieu",
                defaults: new { controller = "AboutUs", action = "Index", Alias = UrlParameter.Optional },
                namespaces: new[] { "DuLichV2.Controllers" }
            );
            routes.MapRoute(
                name: "ContactUs",
                url: "lien-he",
                defaults: new { controller = "ContactUs", action = "Index", Alias = UrlParameter.Optional },
                namespaces: new[] { "DuLichV2.Controllers" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] {"DuLichV2.Controllers"}
            );
        }
    }
}
