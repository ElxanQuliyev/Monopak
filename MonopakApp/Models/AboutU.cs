namespace MonopakApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AboutU
    {
        public int Id { get; set; }

        [StringLength(800)]
        public string AboutPhoto { get; set; }

        [Required]
        [StringLength(400)]
        public string Header { get; set; }

        public string Description { get; set; }
    }
}
