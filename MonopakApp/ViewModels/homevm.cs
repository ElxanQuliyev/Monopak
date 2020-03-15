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
        public IEnumerable<Category> categoryList { get; set; }
        public IEnumerable<Product>ProductList{ get; set; }
    }
}