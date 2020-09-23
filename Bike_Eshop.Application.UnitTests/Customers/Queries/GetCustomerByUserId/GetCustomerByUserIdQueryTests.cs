using System.Threading;
using System.Threading.Tasks;
using Bike_EShop.Application.Common.Exceptions;
using Bike_EShop.Application.Customers.Queries.GetCustomerByUserId;
using NUnit.Framework;
using Shouldly;

namespace Bike_Eshop.Application.UnitTests.Customers.Queries.GetCustomerByUserId
{
    public class GetCustomerByUserIdQueryTests
    {
        private GetCustomerByUserIdQuery.GetCustomerByUserIdQueryHandler _handler;
        private const string ExistingUserId = "000-000-000";

        [SetUp]
        public void SetUp()
        {
            var fixture = new QueryTestFixture();
            _handler = new GetCustomerByUserIdQuery.GetCustomerByUserIdQueryHandler(fixture.Context, fixture.Mapper);
        }

        [Test]
        public async Task Handle_GivenValidId_ReturnsCorrectVmAndObject()
        {
            var query = new GetCustomerByUserIdQuery { UserId =  ExistingUserId};

            var result = await _handler.Handle(query, CancellationToken.None);
            var customer = result.Customer;

            result.ShouldBeOfType<CustomerByUserIdVM>();
            customer.Id.ShouldBe(1);
        }

        [Test]
        public void Handle_GivenInvalidId_ThrowsException()
        {
            var query = new GetCustomerByUserIdQuery { UserId = "not existing userId" };

            Should.ThrowAsync<NotFoundException>(() =>
                _handler.Handle(query, CancellationToken.None));
        }
    }
}
