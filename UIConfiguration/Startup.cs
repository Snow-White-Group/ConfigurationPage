using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UIConfiguration.Startup))]
namespace UIConfiguration
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
