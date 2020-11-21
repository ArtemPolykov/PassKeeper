using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PassKeePerLib.Data;
using PassKeePerLib.Models;
using Microsoft.EntityFrameworkCore;

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
            var passwordOpt = Configuration.GetSection("PasswordOptions");
            var connectionString = Configuration.GetSection("ConnectionStrings")["PassKeeperDatabase"];

            services.AddControllers();

            services.AddDbContext<passkeeperContext>(opt => 
                opt.UseMySql(connectionString));
            
            services.AddIdentityCore<Users>(o => 
            {
                o.Password.RequiredLength = passwordOpt.GetValue<int>("RequiredLength");
                o.Password.RequireNonAlphanumeric = passwordOpt.GetValue<bool>("RequireNonAlphanumeric");
                o.Password.RequireLowercase = passwordOpt.GetValue<bool>("RequireLowercase");
                o.Password.RequireUppercase = passwordOpt.GetValue<bool>("RequireUppercase");
                o.Password.RequireDigit = passwordOpt.GetValue<bool>("RequireDigit");
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

            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
