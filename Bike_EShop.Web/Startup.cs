using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bike_EShop.Application.Common.Extensions;
using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Application.Common.Models;
using Bike_EShop.Application.Common.Settings;
using Bike_EShop.Infrastructure.Data;
using Bike_EShop.Infrastructure.Extensions;
using Bike_EShop.Web.Common.Services;
using Bike_EShop.Web.Models.Product;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bike_EShop.Web
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
            //Adds services container made in Application & Infrastructure Project
            services.AddApplication();
            services.AddInfrastructure(Configuration);

            //Adds DI to IOC Container
            services.AddTransient<IBikeCountService, BikeCountService>();
            services.AddTransient<IBagSessionService, BagSessionService>();

            services.AddSession();

            services.AddControllersWithViews()
                .AddFluentValidation(fv => 
                {
                    fv.RegisterValidatorsFromAssemblyContaining<IApplicationDbContext>();
                    fv.RegisterValidatorsFromAssemblyContaining<ProductDetailViewModelValidator>();
                });

            //Reads discounts from appconfig file & passes it to IOC container
            var discountConfig = Configuration.GetSection("DiscountSettings").Get<DiscountList>();
            services.AddSingleton(discountConfig);

            //Reads emailsettings from appconfig
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
