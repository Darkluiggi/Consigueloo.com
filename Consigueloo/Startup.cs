using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Consigueloo.Startup))]
namespace Consigueloo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
    