using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MonopakApp.Models
{
    public class eCommerceDBInitializer: CreateDatabaseIfNotExists<MonoDB>
    {
        protected override void Seed(MonoDB context)
        {
            SeedConfigurations(context);
        }
        public void SeedConfigurations(MonoDB context)
        {




            Configuration enableCashOnDeliveryMethod = new Configuration()
            {
                Key = "EnableCashOnDeliveryMethod",
                Value = "true",
                Description = "Set value to true to enable Cash on Delivery Method or set value to false to disable Cash on Delivery Method.",
                ConfigurationType = (int)ConfigurationType.Other,
                ModifiedOn = DateTime.Now
            };


            context.Configurations.AddRange(new List<Configuration> { enableCashOnDeliveryMethod});

            context.SaveChanges();
        }

    }
}