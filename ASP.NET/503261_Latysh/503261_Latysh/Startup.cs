using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_503261_Latysh.Startup))]
namespace _503261_Latysh
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
