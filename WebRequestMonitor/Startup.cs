using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using WebRequestMonitor.SignalR;

[assembly: OwinStartup(typeof(WebRequestMonitor.Startup))]

namespace WebRequestMonitor
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.MapSignalR<RequestMonitor>("/monitor");
        }
    }
}
