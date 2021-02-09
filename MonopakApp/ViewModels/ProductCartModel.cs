using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonopakApp.ViewModels
{
    public class ProCart
    {
        public List<ProductCartModel> data { get; set; }
    }
    public class ProductCartModel
    {
        public int productId { get; set; }
        public int quantity { get; set; }
    }
}