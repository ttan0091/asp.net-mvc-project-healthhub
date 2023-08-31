using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Week6.Startup))]
namespace Week6
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
