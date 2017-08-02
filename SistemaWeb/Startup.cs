using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SistemaWeb.Startup))]
namespace SistemaWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
