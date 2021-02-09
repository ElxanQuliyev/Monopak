using MonopakApp.Models;
using MonopakApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonopakApp.Areas.monopakadmin.ViewModels
{
    public class OrdersListingViewModel:PageViewModel
    {
        public List<Order> Orders { get; set; }

        public string UserEmail { get; set; }
        public int? OrderID { get; set; }
        public int? OrderStatus { get; set; }
        public int? TotalOrder { get; set; }
        public Pager Pager { get; set; }
    }
}