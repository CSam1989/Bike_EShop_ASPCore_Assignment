using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bike_EShop.Application.Common.Exceptions;
using Bike_EShop.Application.Products.Commands.Delete;
using NUnit.Framework;
using Shouldly;

namespace Bike_Eshop.Application.UnitTests.Products.Commands.Delete
{
    public class DeleteProductCommandTests: CommandTestBase
    {
        private DeleteProductCommand.DeleteProductCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _handler = new DeleteProductCommand.DeleteProductCommandHandler(Context);
        }

        [Test]
        public async Task Handle_GivenValidId_ShouldRemovePersistedProduct()
        {
            var command = new DeleteProductCommand()
            {
                Id = 1
            };

            await _handler.Handle(command, CancellationToken.None);

            var entity = Context.Products.Find(command.Id);

            entity.ShouldBeNull();
        }

        [Test]
        public void Handle_GivenInvalidId_ThrowsException()
        {
            var command = new DeleteProductCommand
            {
                Id = 99
            };

            Should.ThrowAsync<NotFoundException>(() =>
                _handler.Handle(command, CancellationToken.None));
        }
    }
}
