using System;
using System.IO;
using Bike_EShop.Infrastructure.Data;
using Bike_EShop.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;


namespace Bike_EShop.Web
{
    public class Program
    {
        private const string OutputTemplate = "{NewLine}{Timestamp:HH:mm:ss} [{Level}] ({CorrelationToken}) {Message}{NewLine}{Exception}";
        private static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false, true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
        .AddEnvironmentVariables()
        .Build();

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration, "Serilog")
                .CreateLogger();

            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                DbInitializer.SeedProducts(context).Wait();
                DbInitializer.SeedAdmin(services).Wait();
                DbInitializer.SeedAdminRole(services).Wait();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while seeding the database.");
            }

            try
            {
                Log.Information("Application Starting.");
                host.Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "The Application failed to start.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
