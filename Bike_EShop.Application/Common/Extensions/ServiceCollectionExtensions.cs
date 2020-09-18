using AutoMapper;
using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Application.Common.Services;
using Bike_EShop.Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Bike_EShop.Application.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IRandomGeneratorService, RandomGeneratorService>();
            services.AddTransient<IDiscountService, DiscountService>();

            //adding generic Interface
            services.AddTransient(typeof(IPaginationService<>), typeof(PaginationService<>));
            

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
