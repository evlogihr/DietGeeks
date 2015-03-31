using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DietGeeks.MVC.Startup))]
namespace DietGeeks.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
