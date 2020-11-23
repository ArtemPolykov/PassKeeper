using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PassKeePerLib.Data;
using PassKeePerLib.Models;
using Microsoft.EntityFrameworkCore;
using PassKeeperAuthorizationService.Configuration;

namespace PassKeeperAuthorizationService
{
	public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("PassKeeperDatabase");
            var passwordOpt = Configuration.GetSection("PasswordOptions").Get<PasswordSettings>();
            var tokenParametres = Configuration.GetSection("TokenParametres").Get<TokenParametres>();

            services.AddControllers();

            services.AddSingleton<TokenParametres>(tokenParametres);

            services.AddDbContext<passkeeperContext>(opt => 
                opt.UseMySql(connectionString));
            
            services.AddIdentityCore<Users>(o => 
            {
                o.Password.RequiredLength = passwordOpt.RequiredLength;
                o.Password.RequireNonAlphanumeric = passwordOpt.RequireNonAlphanumeric;
                o.Password.RequireLowercase = passwordOpt.RequireLowercase;
                o.Password.RequireUppercase = passwordOpt.RequireUppercase;
                o.Password.RequireDigit = passwordOpt.RequireDigit;
            })
            .AddEntityFrameworkStores<passkeeperContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
