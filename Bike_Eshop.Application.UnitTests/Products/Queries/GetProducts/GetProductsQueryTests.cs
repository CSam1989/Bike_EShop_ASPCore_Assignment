using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Application.Products.Queries.GetProducts;
using Bike_EShop.Domain.Entities;
using Bike_EShop.Infrastructure.Persistence;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace Bike_Eshop.Application.UnitTests.Products.Queries.GetProducts
{
    public class GetProductsQueryTests
    {
        private Mock<IPaginationService<Product>> _pagination;
        private Mock<IRandomGeneratorService> _randomGenerator;
        private Mock<IBikeCountService> _bikeCount;

        [SetUp]
        public void SetUp()
        {
            var fixture = new QueryTestFixture();
            _context = fixture.Context;
            _mapper = fixture.Mapper;

            _pagination = new Mock<IPaginationService<Product>>();
            _pagination
                .Setup(x =>
                    x.Paginate(It.IsAny<IQueryable<Product>>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(_context.Products.AsQueryable);

            _randomGenerator = new Mock<IRandomGeneratorService>();
            _bikeCount = new Mock<IBikeCountService>();
        }

        private ApplicationDbContext _context;
        private IMapper _mapper;

        [Test]
        public async Task Handle_ReturnsCorrectVmAndListCount()
        {
            var query = new GetProductsQuery();

            var handler = new GetProductsQuery.GetProductsQueryHandler(_context, _mapper, _pagination.Object, _randomGenerator.Object, _bikeCount.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<ProductsVM>();
            result.List.Count().ShouldBe(3);
        }
    }
}
