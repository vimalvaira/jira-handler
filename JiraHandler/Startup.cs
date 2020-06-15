using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JiraHandler.Startup))]
namespace JiraHandler
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
