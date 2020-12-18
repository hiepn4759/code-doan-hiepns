using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebNhaHangOnline.Startup))]
namespace WebNhaHangOnline
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
