using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Application.Common.Models;
using Bike_EShop.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bike_EShop.Application.Products.Queries.GetProducts
{
    public class GetProductsQuery:PaginationDto, IRequest<ProductsVM>
    {
        public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ProductsVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IPaginationService<Product> _pagination;
            private readonly IRandomGeneratorService _randomGenerator;
            private readonly IBikeCountService _bikeCount;

            public GetProductsQueryHandler(
                IApplicationDbContext context, 
                IMapper mapper,
                IPaginationService<Product> pagination,
                IRandomGeneratorService randomGenerator,
                IBikeCountService bikeCount)
            {
                this._context = context;
                this._mapper = mapper;
                this._pagination = pagination;
                this._randomGenerator = randomGenerator;
                this._bikeCount = bikeCount;
            }

            public async Task<ProductsVM> Handle(GetProductsQuery request, CancellationToken cancellationToken)
            {
                var productsQuery = _context.Products.AsQueryable();
                var currentPage = request.CurrentPage ?? 1;
                var paginatedProductsQuery = _pagination.Paginate(productsQuery, currentPage, request.PageSize);

                var vm = new ProductsVM
                {
                    List = await paginatedProductsQuery
                        .ProjectTo<ProductsDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken),

                    Pagination = new PaginationToReturnDto
                    {
                        CurrentPage = currentPage,
                        PageSize = request.PageSize,
                        Count = await productsQuery.CountAsync(cancellationToken)
                    }
                };

                foreach (var product in vm.List)
                {
                    product.BikeNr = _randomGenerator.GenerateRandomPositiveNumber(_bikeCount.Count());
                }

                return vm;
            }
        }
    }
}
