namespace MonopakApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TopSlider")]
    public partial class TopSlider
    {
        public int Id { get; set; }

        [StringLength(800)]
        [DisplayName("Şəkil")]
        public string SliderPhoto { get; set; }

        [StringLength(500)]
        [DisplayName("Mehsul haqqında")]

        public string Description { get; set; }
        [DisplayName("Qiymət")]
        public double? Price { get; set; }
    }
}
