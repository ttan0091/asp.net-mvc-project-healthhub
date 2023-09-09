using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(week7.Startup))]
namespace week7
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
