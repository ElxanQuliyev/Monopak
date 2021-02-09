using MonopakApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonopakApp.ViewModels
{
    public class CartProductsViewModel
    {
        public List<int> ProductIDs { get; set; }
        public List<Product> Products { get; set; }
        public Setting Settings { get; set; }
        public IEnumerable<Category> categoryList { get; set; }

    }
}