namespace MonopakApp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MonoDB : DbContext
    {
        public MonoDB()
            : base("name=MonoDB")
        {
        }

        public virtual DbSet<AboutU> AboutUs { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<TopSlider> TopSliders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
