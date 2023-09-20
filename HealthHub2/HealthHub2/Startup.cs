using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HealthHub2.Startup))]
namespace HealthHub2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
