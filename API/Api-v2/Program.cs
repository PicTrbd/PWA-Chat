using System;
using Api_v2.Controllers;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;


[assembly: OwinStartup(typeof(Api_v2.Startup))]
namespace Api_v2
{
    //.Net Core 2.0 Cela ne marche pas, WebApp est null lorsqu'on lance le programme
    class Program
    {
        static void Main(string[] args)
        {
            Dependencies.ChatController = new ChatController();

            string url = "http://localhost:8080";
            using (WebApp.Start(url))
            {
                Console.WriteLine("Server running on {0}", url);
                Console.ReadLine();
            }
        }
    }

    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR("/chat", new HubConfiguration());
        }
    }


}
