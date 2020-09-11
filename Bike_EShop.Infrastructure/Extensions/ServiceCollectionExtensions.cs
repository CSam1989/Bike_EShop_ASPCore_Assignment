using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Infrastructure.Persistence;
using Bike_EShop.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bike_EShop.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDateTime, DateTimeService>();

            services.AddTransient<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddDbContext<ApplicationDbContext>(opt => opt
            .UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                }));

            return services;
        }
    }
}
