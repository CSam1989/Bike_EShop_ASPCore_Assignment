using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bike_EShop.Application.Common.Interfaces;
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
    public class GetProductsQuery: IRequest<ProductsVM>
    {
        public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ProductsVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetProductsQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                this._context = context;
                this._mapper = mapper;
            }

            public async Task<ProductsVM> Handle(GetProductsQuery request, CancellationToken cancellationToken)
            {
                var vm = new ProductsVM
                {
                    List = await _context.Products
                                .ProjectTo<ProductsDto>(_mapper.ConfigurationProvider)
                                .ToListAsync(cancellationToken)
                };

                return vm;
            }
        }
    }
}
