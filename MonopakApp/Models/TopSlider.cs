namespace MonopakApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TopSlider")]
    public partial class TopSlider
    {
        public int Id { get; set; }

        [StringLength(800)]
        public string SliderPhoto { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public double? Price { get; set; }
    }
}
