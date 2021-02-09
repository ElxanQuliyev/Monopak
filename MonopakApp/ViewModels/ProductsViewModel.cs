using MonopakApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonopakApp.ViewModels
{
    public class ProductsViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public int? CategoryID { get; set; }
        public string CategoryName { get; set; }
        public List<Category> SearchedCategories { get; set; }

        public string SearchTerm { get; set; }

        public bool isPartial { get; set; }

        public Pager Pager { get; set; }
        public int PageNo { get; set; }
        public int? PageSize { get; set; }

    }
    public class FilterProductsViewModel
    {
        public string SearchTerm { get; set; }
        public List<Product> Products { get; set; }
        public Pager Pager { get; set; }
    }
}