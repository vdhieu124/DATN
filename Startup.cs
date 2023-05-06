using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DuLichV2.Startup))]
namespace DuLichV2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
