namespace MonopakApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Setting
    {
        public int Id { get; set; }

        [StringLength(80)]
        public string AdminEmail { get; set; }

        [StringLength(250)]
        public string AdminPassword { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [StringLength(300)]
        public string Adress { get; set; }

        public string Fblink { get; set; }

        public string InstaLink { get; set; }

        public string Description { get; set; }

        [StringLength(250)]
        public string LocX { get; set; }

        [StringLength(250)]
        public string LocY { get; set; }
    }
}
