using MonopakApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonopakApp.Areas.monopakadmin.ViewModels
{
    public class OrderDetailsViewModel:PageViewModel
    {
        public Order Order { get; set; }
        public ApplicationUser Customer { get; set; }
    }
}