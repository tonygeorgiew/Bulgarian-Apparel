using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bulgarian_Apparel.Web.Startup))]
namespace Bulgarian_Apparel.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
