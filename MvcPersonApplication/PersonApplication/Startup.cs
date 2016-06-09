using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PersonApplication.Startup))]
namespace PersonApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
