namespace MonopakApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        [DisplayName("Məhsulun adı")]
        public string Name { get; set; }

        [StringLength(800)]
        [DisplayName("Şəkil")]

        public string ProductPhoto { get; set; }

        [StringLength(600)]
        [DisplayName("Məzmun")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
