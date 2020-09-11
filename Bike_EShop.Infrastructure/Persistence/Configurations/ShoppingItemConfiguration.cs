using Bike_EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bike_EShop.Infrastructure.Persistence.Configurations
{
    public class ShoppingItemConfiguration : IEntityTypeConfiguration<ShoppingItem>
    {
        public void Configure(EntityTypeBuilder<ShoppingItem> builder)
        {
            builder
                .HasOne(o => o.Product)
                .WithMany(p => p.ShoppingItems)
                .IsRequired()
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(o => o.Bag)
                .WithMany(o => o.Items)
                .IsRequired()
                .HasForeignKey(o => o.ShoppingBagId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
