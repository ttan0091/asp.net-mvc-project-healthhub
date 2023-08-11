using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(week4_FIT5032_MyModelFirst.Startup))]
namespace week4_FIT5032_MyModelFirst
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
