using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bike_EShop.Application.Common.Exceptions;
using Bike_EShop.Application.ShoppingItems.Commands.Create;
using Bike_EShop.Domain.Entities;
using NUnit.Framework;
using Shouldly;

namespace Bike_Eshop.Application.UnitTests.ShoppingItems.Commands.Upsert
{
    public class UpsertShoppingItemCommandTests : CommandTestBase
    {
        private UpsertShoppingItemCommand.UpsertShoppingItemCommandHandler _handler;
        private const int Quantity = 1;

        [SetUp]
        public void Setup()
        {
            _handler = new UpsertShoppingItemCommand.UpsertShoppingItemCommandHandler(Context);
        }

        [Test]
        public async Task Handle_GivenInvalidProductIdAndBagId_ShouldCreateItemCount()
        {
            var command = new UpsertShoppingItemCommand()
            {
                BagId = 1,
                ProductId = 3,
                Quantity = Quantity
            };

            var result = await _handler.Handle(command, CancellationToken.None);

            var entity = Context.ShoppingItems.Find(result);

            entity.ShouldNotBeNull();
            entity.ShoppingBagId.ShouldBe(command.BagId);
            entity.ProductId.ShouldBe(command.ProductId);
            entity.Quantity.ShouldBe(command.Quantity);
        }

        [Test]
        public async Task Handle_GivenValidProductIdAndBagId_ShouldUpdateItemCount()
        {
            var previousShoppingItemCount = Context.ShoppingItems.Find(1).Quantity;

            var command = new UpsertShoppingItemCommand()
            {
                BagId = 1,
                ProductId = 1,
                Quantity = Quantity
            };

            var result = await _handler.Handle(command, CancellationToken.None);

            var entity = Context.ShoppingItems.Find(result);

            entity.ShouldNotBeNull();
            entity.ShoppingBagId.ShouldBe(command.BagId);
            entity.ProductId.ShouldBe(command.ProductId);
            entity.Quantity.ShouldBe(previousShoppingItemCount + command.Quantity);
        }
    }
}
