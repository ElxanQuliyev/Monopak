using MonopakApp.Models;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MonopakApp.Helpers
{
    public static class HtmlHelpers
    {
        public static string getCellBackgroundClassByOrderStatus(this HtmlHelper htmlHelper, OrderStatus orderStatus)
        {
            var bgClass = string.Empty;

            switch (orderStatus)
            {
                case OrderStatus.Yeni:
                    bgClass = "bg-primary text-white";
                    break;
                case OrderStatus.ProsesGedir:
                    bgClass = "bg-info text-white";
                    break;
                case OrderStatus.Catdırıldı:
                    bgClass = "bg-success text-white";
                    break;
                case OrderStatus.UgursuzOldu:
                case OrderStatus.LevgEdildi:
                    bgClass = "bg-danger text-white";
                    break;
                case OrderStatus.Gözlemede:
                case OrderStatus.GeriQaytarıldı:
                    bgClass = "bg-warning";
                    break;
                default:
                    bgClass = string.Empty;
                    break;
            }

            return bgClass;
        }
        public static string Orders(this UrlHelper helper, string userEmail = "", int? orderID = 0, int? orderStatus = 0, int? pageNo = 0)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", "AdminOrders");

            if (!string.IsNullOrEmpty(userEmail))
            {
                routeValues.Add("userEmail", userEmail);
            }

            if (orderID.HasValue && orderID.Value > 0)
            {
                routeValues.Add("orderID", orderID.Value);
            }

            if (orderStatus.HasValue && orderStatus.Value > 0)
            {
                routeValues.Add("orderStatus", orderStatus.Value);
            }

            if (pageNo.HasValue && pageNo.Value > 1)
            {
                routeValues.Add("pageNo", pageNo.Value);
            }

            routeURL = helper.RouteUrl("EntityList", routeValues);

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

    }
}