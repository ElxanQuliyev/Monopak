using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MonopakApp.Helpers
{
    public static class URLHelper
    {
        public static string SearchProducts(this UrlHelper helper, string category = "", string q = "",string sortby = "", int? pageNo = 0, int? pageSize = 0)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("category", category);

            if (!string.IsNullOrEmpty(q))
            {
                routeValues.Add("q", q);
            }
            
            if (!string.IsNullOrEmpty(sortby))
            {
                routeValues.Add("sortby", sortby);
            }

            if (pageNo.HasValue && pageNo.Value > 1)
            {
                routeValues.Add("pageNo", pageNo.Value);
            }

            if (pageSize.HasValue && pageSize.Value > 1)
            {
                routeValues.Add("pageSize", pageSize.Value);
            }

            routeURL = helper.RouteUrl("SearchProducts", routeValues);

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }
    }
}