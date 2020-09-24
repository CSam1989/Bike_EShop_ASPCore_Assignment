using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bike_EShop.Application.Common.Exceptions;
using Bike_EShop.Application.Products.Queries.GetProductById;
using NUnit.Framework;
using Shouldly;

namespace Bike_Eshop.Application.UnitTests.Products.Queries.GetProductById
{
    public class GetProductByIdQueryTests
    {
        private GetProductByIdQuery.GetProductByIdQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            var fixture = new QueryTestFixture();
            _handler = new GetProductByIdQuery.GetProductByIdQueryHandler(fixture.Context, fixture.Mapper);
        }

        [Test]
        public async Task Handle_GivenValidId_ReturnsCorrectVmAndObject()
        {
            var query = new GetProductByIdQuery { Id = 1 };

            var result = await _handler.Handle(query, CancellationToken.None);
            var product = result.Product;

            result.ShouldBeOfType<ProductByIdVM>();
            product.Id.ShouldBe(1);
        }

        [Test]
        public void Handle_GivenInvalidId_ThrowsException()
        {
            var query = new GetProductByIdQuery { Id = 99 };

            Should.ThrowAsync<NotFoundException>(() =>
                _handler.Handle(query, CancellationToken.None));
        }
    }
}
