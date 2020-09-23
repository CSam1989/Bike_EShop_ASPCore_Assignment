using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bike_EShop.Application.Common.Exceptions;
using Bike_EShop.Application.Products.Commands.Upsert;
using NUnit.Framework;
using Shouldly;

namespace Bike_Eshop.Application.UnitTests.Products.Commands.Upsert
{
    public class UpsertProductCommandTests: CommandTestBase
    {
        private UpsertProductCommand.UpsertProductCommandHandler _handler;
        private const string BikeRegistrationNumber = "ABC12345";
        private const string Name = "TestBike";
        private const decimal Price = 123;

        [SetUp]
        public void Setup()
        {
            _handler = new UpsertProductCommand.UpsertProductCommandHandler(Context);
        }

        [Test]
        public async Task Handle_GivenNoId_ShouldCreateProduct()
        {
            var command = new UpsertProductCommand
            {
                BikeRegistrationNumber = BikeRegistrationNumber,
                Name = Name,
                Price = Price
            };

            var result = await _handler.Handle(command, CancellationToken.None);

            var entity = Context.Products.Find(result);

            entity.ShouldNotBeNull();
            entity.BikeRegistrationNumber.ShouldBe(command.BikeRegistrationNumber);
            entity.Name.ShouldBe(command.Name);
            entity.Price.ShouldBe(command.Price);
        }

        [Test]
        public async Task Handle_GivenValidId_ShouldUPdatePersistedBenaming()
        {
            var command = new UpsertProductCommand
            {
                Id = 1,
                BikeRegistrationNumber = BikeRegistrationNumber,
                Name = Name,
                Price = Price
            };

            await _handler.Handle(command, CancellationToken.None);

            var entity = Context.Products.Find(command.Id);

            entity.ShouldNotBeNull();
            entity.BikeRegistrationNumber.ShouldBe(command.BikeRegistrationNumber);
            entity.Name.ShouldBe(command.Name);
            entity.Price.ShouldBe(command.Price);
        }

        [Test]
        public void Handle_GivenInvalidId_ThrowsException()
        {
            var command = new UpsertProductCommand
            {
                Id = 99,
                BikeRegistrationNumber = BikeRegistrationNumber,
                Name = Name,
                Price = Price
            };

            Should.ThrowAsync<NotFoundException>(() =>
                _handler.Handle(command, CancellationToken.None));
        }
    }
}
