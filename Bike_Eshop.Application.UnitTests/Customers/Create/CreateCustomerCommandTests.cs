using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bike_EShop.Application.Customers.Commands.Create;
using NUnit.Framework;
using Shouldly;

namespace Bike_Eshop.Application.UnitTests.Customers.Create
{
    public class CreateCustomerCommandTests : CommandTestBase
    {
        [Test]
        public async Task Handle_ShouldPersistCustomer()
        {
            var command = new CreateCustomerCommand
            {
                FirstName = "Sam",
                Name = "Ceustermans",
                UserId = "123-456-789"
            };

            var handler = new CreateCustomerCommand.CreateCustomerCommandHandler(Context);

            var result = await handler.Handle(command, CancellationToken.None);

            var entity = Context.Customers.Find(result);

            entity.ShouldNotBeNull();
            entity.FirstName.ShouldBe(command.FirstName);
            entity.Name.ShouldBe(command.Name);
            entity.UserId.ShouldBe(command.UserId);
        }
    }
}
