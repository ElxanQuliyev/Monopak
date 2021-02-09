using MonopakApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonopakApp.ViewModels
{
    public class HomeVm
    {
        public IEnumerable<TopSlider> TopSlider{ get; set; }
        public AboutU about { get; set; }
        public Setting Settings{ get; set; }
        public List<Category> CategoryList { get; set; }
        public IEnumerable<Product>ProductList{ get; set; }
        public Pager Pager { get; set; }
        public int PageNo { get; set; }
        public int? PageSize { get; set; }

    }
}