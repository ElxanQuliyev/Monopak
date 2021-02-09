using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MonopakApp.Startup))]
namespace MonopakApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
