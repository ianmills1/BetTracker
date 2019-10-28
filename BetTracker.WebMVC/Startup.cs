using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BetTracker.WebMVC.Startup))]
namespace BetTracker.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
