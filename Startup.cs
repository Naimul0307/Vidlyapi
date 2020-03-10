using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VidlyApi.Startup))]
namespace VidlyApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
