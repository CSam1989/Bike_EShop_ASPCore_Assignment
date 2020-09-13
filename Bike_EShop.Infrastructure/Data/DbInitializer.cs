using Bike_EShop.Domain.Entities;
using Bike_EShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bike_EShop.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Bike_EShop.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void SeedProducts(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Products.Any())
                return; //Als products niet leeg is, dan moet de db niet geseed worden

            var productsJson =
                File.ReadAllText(
                    "../Bike_EShop.Infrastructure/Data/product_generatedData.json");

            var products = JsonConvert.DeserializeObject<List<Product>>(productsJson);

            foreach (var product in products) context.Products.Add(product);

            context.SaveChanges();
        }

        public static void SeedAdmin(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            context.Database.EnsureCreated();

            if (context.Customers.Any(c => c.Name.ToLower() =="admin"))
                return; //Als customers een admin bevat, dan moet de db niet geseed worden

            var user = new ApplicationUser
            {
                UserName = "Admin",
                Email = "Admin@Example.com",
                Customer = new Customer
                {
                    FirstName = "",
                    Name = "Admin"
                }
            };
            const string password = "Admin123*";

            var result = userManager.CreateAsync(user, password).Result;

            if (!result.Succeeded) return;
            user.Customer.UserId = user.Id;
            context.SaveChanges();
        }
    }
}
