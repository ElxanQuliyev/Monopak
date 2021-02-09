using MonopakApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonopakApp.Services
{
    public class ConfigurationsService
    {
        #region Define as Singleton
        private static ConfigurationsService _Instance;

        public static ConfigurationsService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ConfigurationsService();
                }

                return (_Instance);
            }
        }

        private ConfigurationsService()
        {
        }
        #endregion
        public List<Configuration> GetConfigurationsByType(int configurationType)
        {
            MonoDB context = new MonoDB();

            return context.Configurations.Where(x => x.ConfigurationType == configurationType).ToList();
        }
        public Configuration GetConfigurationByKey(string key)
        {
            MonoDB context = new MonoDB();

            return context.Configurations.FirstOrDefault(x => x.Key == key);
        }
    
        public void UpdateConfiguration(Configuration configuration)
        {
            MonoDB context = new MonoDB();

            context.Entry(configuration).State = System.Data.Entity.EntityState.Modified;

            context.SaveChanges();
        }

        public void UpdateConfigurationValue(string key, string value)
        {
            MonoDB context = new MonoDB();

            var configuration = context.Configurations.Find(key);

            configuration.Value = value;

            context.Entry(configuration).State = System.Data.Entity.EntityState.Modified;

            context.SaveChanges();
        }

        public List<Configuration> SearchConfigurations(int? configurationType, string searchTerm, int? pageNo, int pageSize)
        {
            MonoDB context = new MonoDB();

            var configurations = context.Configurations.AsQueryable();

            if (configurationType.HasValue && configurationType.Value > 0)
            {
                configurations = configurations.Where(x => x.ConfigurationType == configurationType.Value);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                configurations = configurations.Where(x => x.Key.ToLower().Contains(searchTerm.ToLower()));
            }

            pageNo = pageNo ?? 1;
            var skipCount = (pageNo.Value - 1) * pageSize;

            return configurations.OrderBy(x => x.Key).Skip(skipCount).Take(pageSize).ToList();
        }

        public int GetConfigurationsCount(int? configurationType, string searchTerm)
        {
            MonoDB context = new MonoDB();

            var configurations = context.Configurations.AsQueryable();

            if (configurationType.HasValue && configurationType.Value > 0)
            {
                configurations = configurations.Where(x => x.ConfigurationType == configurationType.Value);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                configurations = configurations.Where(x => x.Key.ToLower().Contains(searchTerm.ToLower()));
            }

            return configurations.Count();
        }
    }
}