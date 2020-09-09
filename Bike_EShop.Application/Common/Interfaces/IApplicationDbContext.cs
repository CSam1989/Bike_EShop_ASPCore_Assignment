using Bike_EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bike_EShop.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ShoppingBag> ShoppingBags { get; set; }
        DbSet<ShoppingItem> ShoppingItems { get; set; }
    }
}