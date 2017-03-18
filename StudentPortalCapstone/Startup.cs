using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentPortalCapstone.Startup))]
namespace StudentPortalCapstone
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
