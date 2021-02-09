using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonopakApp.Areas.monopakadmin.ViewModels
{
    public class AdminHomeVm
    {
        public int? TotalOrder { get; set; }
        public int? CategoryCount { get; set; }
        public int? ProductCount{ get; set; }
        //public int UserCount { get; set; }
    }
}