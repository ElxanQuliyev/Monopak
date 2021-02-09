using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonopakApp.Models
{
    public class OrderHistory
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int OrderStatus { get; set; }
        public string Note { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }
}