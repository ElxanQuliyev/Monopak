using MonopakApp.Services;

namespace MonopakApp.Helpers
{
    public class ConfigurationsHelper
    {
        private static int? _DashboardRecordsSizePerPage { get; set; }
        public static int DashboardRecordsSizePerPage
        {
            get
            {
                if (!_DashboardRecordsSizePerPage.HasValue)
                {
                    var config = ConfigurationsService.Instance.GetConfigurationByKey("DashboardRecordsSizePerPage");

                    if (config != null)
                    {
                        _DashboardRecordsSizePerPage = int.Parse(config.Value);
                    }
                    else _DashboardRecordsSizePerPage = 10;
                }

                return _DashboardRecordsSizePerPage.Value;
            }
        }
    }
}