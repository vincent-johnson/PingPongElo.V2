using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PingPong.Web.Startup))]
namespace PingPong.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
