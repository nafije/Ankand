
using Ankand.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Sockets;
using Microsoft.Data.SqlClient;
using Ankand.Models;
using Ankand.Data;
using Ankand.Data.Services;
using Ankand.Data.Services;

namespace e_Book
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            services.AddDataProtection();
            services.AddDistributedMemoryCache();
            services.AddControllers();
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("Data Source=nafije-pc\\sqlexpress;Initial Catalog=Ankand-db;Integrated Security=True;Pooling=False; trustServerCertificate = true"));
            }, ServiceLifetime.Scoped);
            services.AddScoped<DbContextOptions<AppDbContext>>();
            services.AddScoped<AppDbContext>();
            //Services configuration
            services.AddScoped<IProduktService, ProduktiService>();
            //services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            services.AddMemoryCache();
            services.AddSession();
            services.AddAuthentication();
            services.AddControllersWithViews();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            //app.Use(async (context, next) =>
            //{
            //    if (context.User.Identity.IsAuthenticated)
            //    {
            //        context.Response.Redirect("/Produkti");
            //    }
            //    else
            //    {
            //        context.Response.Redirect("/Account/Login");
            //    }
            //    await next();
            //});
            
            //app.Use(async (context, next) =>
            //{
            //    if (!context.Request.Path.HasValue || context.Request.Path == "/")
            //    {
            //        context.Response.Redirect("/Account/Login");
            //        return;
            //    }

            //    await next();
            //});
                app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            //Authentication & Authorization
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                 name: "default",
                 pattern: "{controller=Books}/{action=Index}/{id?}");
            });
        }

    }
}
