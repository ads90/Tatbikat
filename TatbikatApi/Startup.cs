using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TatbikatApi.Startup))]

namespace TatbikatApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}