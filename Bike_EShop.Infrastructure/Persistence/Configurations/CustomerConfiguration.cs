using Bike_EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Bike_EShop.Domain.Identity;

namespace Bike_EShop.Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(c => c.FirstName)
                .HasMaxLength(50);

            builder.Property(c => c.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .HasOne(c => c.ApplicationUser)
                .WithOne(a => a.Customer)
                .HasForeignKey<Customer>(c => c.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
