using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Applied_MVC_Concepts_I.Startup))]
namespace Applied_MVC_Concepts_I
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
