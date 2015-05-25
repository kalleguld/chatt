using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(webb.Startup))]
namespace webb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
