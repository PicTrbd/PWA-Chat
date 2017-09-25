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
            var builder = new ConfigurationBuilder();

            if (env.IsDevelopment())
                builder.AddUserSecrets<Startup>();

            Configuration = builder.Build();
            SetSecrets();
        }

        private void SetSecrets()
        {
            DatabaseSecrets.UserId = Configuration["UserId"];
            DatabaseSecrets.Password = Configuration["Password"];
            DatabaseSecrets.ServerURL = Configuration["ServerURL"];
            DatabaseSecrets.DatabaseName = Configuration["Database"];
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
