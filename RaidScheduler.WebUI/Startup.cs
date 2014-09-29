using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RaidScheduler.Startup))]
namespace RaidScheduler
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
