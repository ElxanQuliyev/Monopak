using MonopakApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonopakApp.ViewModels
{
    public class BaseViewModel
    {
        public IEnumerable<Category> CategoryList { get; set; }

    }
    public class CartItemsViewModel
    {
        public List<Product> CartItems { get; set; }
        public List<int> ProductIDs { get; set; }
    
    }
    public class CheckoutViewModel : CartItemsViewModel
    {
        public bool CartHasProducts { get; set; }
        public Setting Settings { get; set; }
        public ApplicationUser User{ get; set; }

        //public eCommerceUser User { get; set; }
        public List<ProductCartModel> FinalAmount { get; set; }
    }

}