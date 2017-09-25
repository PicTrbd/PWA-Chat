using ApiCore.Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ApiCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Dependencies.Configure();
            Dependencies.BuildContainer();
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
