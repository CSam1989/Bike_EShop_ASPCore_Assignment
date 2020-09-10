using AutoMapper;
using Bike_EShop.Application.Common.Exceptions;
using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bike_EShop.Application.Products.Queries.GetProductById
{
    public class GetProductByIdQuery: IRequest<ProductByIdVM>
    {
        public int Id { get; set; }

        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductByIdVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetProductByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                this._context = context;
                this._mapper = mapper;
            }

            public async Task<ProductByIdVM> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                var product = await _context.Products.FindAsync(request.Id, cancellationToken);

                if (product is null)
                    throw new NotFoundException(nameof(Product), request.Id);

                return new ProductByIdVM
                {
                    Product = _mapper.Map<ProductByIdDto>(product)
                };
            }
        }
    }
}
