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

            await context.Database.EnsureCreatedAsync();

            if (await context.Customers.AnyAsync(x => string.Equals(x.Name, Admin.Name, StringComparison.CurrentCultureIgnoreCase)))
                return; //Als customers een admin bevat, dan moet de db niet geseed worden

            var user = new ApplicationUser
            {
                UserName = Admin.Name,
                Email = Admin.Email,
                Customer = new Customer
                {
                    FirstName = string.Empty,
                    Name = Admin.Name
                }
            };

            var result = await userManager.CreateAsync(user, Admin.Password);

            if (!result.Succeeded) 
                return;

            user.Customer.UserId = user.Id;
            await context.SaveChangesAsync();
        }

        public static async Task SeedAdminRole(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            //Admin Rol Toevoegen
            var roleCheck = await roleManager.RoleExistsAsync(Admin.Role);
            if (!roleCheck) 
                await roleManager.CreateAsync(new IdentityRole(Admin.Role));

            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == Admin.Email);

            if (user == null) 
                return;

            var roles = context.UserRoles;
            var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == Admin.Role);

            if (adminRole == null) 
                return;

            if (await roles.AnyAsync(ur => ur.UserId == user.Id && ur.RoleId == adminRole.Id)) 
                return;

            roles.Add(new IdentityUserRole<string> { UserId = user.Id, RoleId = adminRole.Id });
            await context.SaveChangesAsync();
        }
    }
}
