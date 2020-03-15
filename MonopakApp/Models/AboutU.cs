namespace MonopakApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AboutU
    {
        public int Id { get; set; }

        [StringLength(800)]
        [DisplayName("Şəkil")]

        public string AboutPhoto { get; set; }

        [StringLength(400)]
        [Required(ErrorMessage = "{0} boş göndərməyin")]
        [DisplayName("Başlıq")]
        public string Header { get; set; }
        [DisplayName("Məzmun")]

        public string Description { get; set; }
    }
}
