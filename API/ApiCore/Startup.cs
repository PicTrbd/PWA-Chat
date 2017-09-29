using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiCore
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();         
       
            Configuration = builder.Build();
            SetSecrets();
        }

        private void SetSecrets()
        {
            DatabaseSecrets.UserId = Configuration["DbAuthentication:UserId"];
            DatabaseSecrets.Password = Configuration["DbAuthentication:Password"];
            DatabaseSecrets.ServerURL = Configuration["DbAuthentication:ServerURL"];
            DatabaseSecrets.DatabaseName = Configuration["DbAuthentication:Database"];

            if (string.IsNullOrEmpty(DatabaseSecrets.UserId))
                throw new Exception("You need to fill your settings file with your DB secrets !");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
            services.AddDbContext<DataAccess>(); 
            services.AddMvc();
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors("CorsPolicy");
            app.UseSignalR(routes => routes.MapHub<ChatHub>("chat"))
               .UseMvc();
        }
    }
}
