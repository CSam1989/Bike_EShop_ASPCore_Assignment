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
                new Product() {Id = 1, Name = "Bike1", Price = 100, BikeRegistrationNumber = "C3"});

            context.SaveChanges();
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
