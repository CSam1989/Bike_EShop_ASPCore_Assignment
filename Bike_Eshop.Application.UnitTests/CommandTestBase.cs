using System;
using System.Collections.Generic;
using System.Text;
using Bike_EShop.Infrastructure.Persistence;

namespace Bike_Eshop.Application.UnitTests
{
    public class CommandTestBase : IDisposable
    {
        public CommandTestBase()
        {
            Context = ApplicationDbContextFactory.Create();
        }

        public ApplicationDbContext Context { get; }

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(Context);
        }
    }
}
