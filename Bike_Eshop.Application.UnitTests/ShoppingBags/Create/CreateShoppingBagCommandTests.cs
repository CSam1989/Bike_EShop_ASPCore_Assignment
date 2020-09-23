using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Application.Shoppingbags.Commands.Create;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace Bike_Eshop.Application.UnitTests.ShoppingBags.Create
{
    public class CreateShoppingBagCommandTests: CommandTestBase
    {
        private CreateShoppingBagCommand.CreateShoppingBagCommandHandler _handler;
        private readonly DateTime _today = new DateTime(2020,9,22);
        private Mock<IDateTime> _datetime;

        [SetUp]
        public void Setup()
        {
            _datetime = new Mock<IDateTime>();
            _datetime.Setup(x => x.Now)
                .Returns(_today);

            _handler = new CreateShoppingBagCommand.CreateShoppingBagCommandHandler(Context, _datetime.Object);
        }
        
        [Test]
        public async Task Handle_ShouldPersistShoppingBag()
        {
            var command = new CreateShoppingBagCommand
            {
                CustomerId = 1
            };

            var result = await _handler.Handle(command, CancellationToken.None);

            var entity = Context.ShoppingBags.Find(result);

            entity.ShouldNotBeNull();
            entity.CustomerId.ShouldBe(command.CustomerId);
            entity.Date.ShouldBe(_today);
        }
    }
}
