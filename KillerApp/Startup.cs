using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KillerApp.Startup))]
namespace KillerApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
