using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Bike_EShop.Application.Common.Mappings;
using Bike_EShop.Infrastructure.Persistence;

namespace Bike_Eshop.Application.UnitTests
{
    public class QueryTestFixture : IDisposable
    {
        public QueryTestFixture()
        {
            Context = ApplicationDbContextFactory.Create();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });

            Mapper = configurationProvider.CreateMapper();
        }
        public ApplicationDbContext Context { get; }

        public IMapper Mapper { get; }

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(Context);
        }
    }
}
