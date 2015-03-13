using Microsoft.Owin;
using MixERP.Net.Web.UI;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace MixERP.Net.Web.UI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
