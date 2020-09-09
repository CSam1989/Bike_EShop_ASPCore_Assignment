using Bike_EShop.Domain.Entities;
using Bike_EShop.Infrastructure.Persistence;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Bike_EShop.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Seed(ApplicationDbContext context)
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
    }
}
