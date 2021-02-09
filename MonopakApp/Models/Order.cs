namespace MonopakApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {

        public int ID { get; set; }     

        [Required]
        [StringLength(128)]
        public string UserID { get; set; }

        public DateTime OrderedAt { get; set; }

        [StringLength(150)]
        public string Status { get; set; }

        [StringLength(400)]
        public string DeliveredAddress { get; set; }
        public string CustomerFirstname { get; set; }
        public string CustomerLastname { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerEmail { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }
        public virtual List<OrderHistory> OrderHistory { get; set; }

    }
}
