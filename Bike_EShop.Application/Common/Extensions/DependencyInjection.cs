using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Application.Common.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Bike_EShop.Application.Common.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IRandomGeneratorService, RandomGeneratorService>();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
