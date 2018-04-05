using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCClientForWebAPI.Startup))]
namespace MVCClientForWebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
