using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IOLSWikiWebsite.Startup))]
namespace IOLSWikiWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
