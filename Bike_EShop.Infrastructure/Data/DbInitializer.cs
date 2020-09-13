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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bike_EShop.Infrastructure.Data
{
    public static class DbInitializer
    {
        private const string Email = "Admin@example.com";

        public static async Task SeedProducts(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Products.Any())
                return; //Als products niet leeg is, dan moet de db niet geseed worden

            var productsJson =
                File.ReadAllText(
                    "../Bike_EShop.Infrastructure/Data/product_generatedData.json");

            var products = JsonConvert.DeserializeObject<List<Product>>(productsJson);

            foreach (var product in products) context.Products.Add(product);

            await context.SaveChangesAsync();
        }

        public static async Task SeedAdmin(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            context.Database.EnsureCreated();

            if (await context.Customers.AnyAsync(c => c.Name.ToLower() =="admin"))
                return; //Als customers een admin bevat, dan moet de db niet geseed worden

            var user = new ApplicationUser
            {
                UserName = "Admin",
                Email = Email,
                Customer = new Customer
                {
                    FirstName = "",
                    Name = "Admin"
                }
            };
            const string password = "Admin123*";

            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded) 
                return;

            user.Customer.UserId = user.Id;
            await context.SaveChangesAsync();
        }

        public static async Task SeedAdminRole(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            const string role = "Admin";

            //Admin Rol Toevoegen
            var roleCheck = await roleManager.RoleExistsAsync(role);
            if (!roleCheck) 
                await roleManager.CreateAsync(new IdentityRole(role));

            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == Email);

            if (user == null) 
                return;

            var roles = context.UserRoles;
            var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == role);

            if (adminRole == null) 
                return;

            if (await roles.AnyAsync(ur => ur.UserId == user.Id && ur.RoleId == adminRole.Id)) 
                return;

            roles.Add(new IdentityUserRole<string> { UserId = user.Id, RoleId = adminRole.Id });
            await context.SaveChangesAsync();
        }
    }
}
