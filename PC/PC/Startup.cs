using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PC.Startup))]
namespace PC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
