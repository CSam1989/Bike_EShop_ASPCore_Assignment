using System;
using System.Collections.Generic;
using System.Text;
using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Domain.Entities;
using Bike_EShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Bike_Eshop.Application.UnitTests
{
    public static class ApplicationDbContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);

            context.Database.EnsureCreated();

            SeedSampleData(context);

            return context;
        }

        public static void SeedSampleData(ApplicationDbContext context)
        {
            context.Products.AddRange(
                new Product() {Id = 1, Name = "Bike1", Price = 100, BikeRegistrationNumber = "A1"},
                new Product() {Id = 2, Name = "Bike2", Price = 200, BikeRegistrationNumber = "B2"},
                new Product() {Id = 3, Name = "Bike3", Price = 300, BikeRegistrationNumber = "C3"});

            context.Customers.Add(new Customer()
            {
                Id = 1, Name = "Test", FirstName = "User", UserId = "000-000-000"
            });

            context.ShoppingBags.Add(
                new ShoppingBag()
                {
                    Id = 1,
                    CustomerId = 1,
                    Date = new DateTime(2020, 9, 22),
                });

            context.ShoppingItems.AddRange(
                new ShoppingItem()
                {
                    ProductId = 1,
                    ShoppingBagId = 1,
                    Quantity = 1
                }, new ShoppingItem()
                {
                    ProductId = 2,
                    ShoppingBagId = 1,
                    Quantity = 2
                });

            context.SaveChanges();
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
