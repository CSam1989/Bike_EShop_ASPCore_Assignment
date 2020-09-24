using AutoMapper;
using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Application.Common.Services;
using Bike_EShop.Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using Bike_EShop.Application.Common.Behaviours;
using Bike_EShop.Application.Common.Factories;

namespace Bike_EShop.Application.Common.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IRandomGeneratorService, RandomGeneratorService>();
            services.AddTransient<IDiscountService, DiscountService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IMailFactory, MailFactory>();

            //adding generic Interface
            services.AddTransient(typeof(IPaginationService<>), typeof(PaginationService<>));

            //adding pipeline behaviour
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));


            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
