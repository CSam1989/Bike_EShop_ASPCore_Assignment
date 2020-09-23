using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bike_EShop.Application.Common.Exceptions;
using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Application.Shoppingbags.Queries.GetBagById;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace Bike_Eshop.Application.UnitTests.ShoppingBags.Queries.GetBagById
{
    public class GetBagByIdQueryTests
    {
        private GetShoppingBagByIdQuery.GetShoppingBagByIdQueryHandler _handler;
        [SetUp]
        public void SetUp()
        {
            var fixture = new QueryTestFixture();
            var discountService = new Mock<IDiscountService>();
            _handler = new GetShoppingBagByIdQuery.GetShoppingBagByIdQueryHandler(fixture.Context, fixture.Mapper, discountService.Object);
        }

        [Test]
        public async Task Handle_GivenValidId_ReturnsCorrectVmAndObject()
        {
            var query = new GetShoppingBagByIdQuery { BagId = 1 };

            var result = await _handler.Handle(query, CancellationToken.None);
            var shoppingBag = result.Bag;

            result.ShouldBeOfType<ShoppingBagByIdVm>();
            shoppingBag.Id.ShouldBe(1);
        }

        [Test]
        public void Handle_GivenInvalidId_ThrowsException()
        {
            var query = new GetShoppingBagByIdQuery { BagId = 99 };

            Should.ThrowAsync<NotFoundException>(() =>
                _handler.Handle(query, CancellationToken.None));
        }
    }
}
