using AutoMapper;
using Bike_EShop.Application.Common.Exceptions;
using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bike_EShop.Application.Shoppingbags.Queries.GetBagById
{
    public class GetShoppingBagByIdQuery: IRequest<ShoppingBagByIdVm>
    {
        public int BagId { get; set; }

        public class GetShoppingBagByIdQueryHandler : IRequestHandler<GetShoppingBagByIdQuery, ShoppingBagByIdVm>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetShoppingBagByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                this._context = context;
                this._mapper = mapper;
            }

            public async Task<ShoppingBagByIdVm> Handle(GetShoppingBagByIdQuery request, CancellationToken cancellationToken)
            {
                var vm = new ShoppingBagByIdVm();

                var bag = await _context.ShoppingBags
                                .Include(s => s.Customer)
                                .Include(s => s.Items)
                                .ThenInclude(i => i.Product)
                                .FirstOrDefaultAsync(s => s.Id == request.BagId, cancellationToken);

                if (bag is null)
                    throw new NotFoundException(nameof(ShoppingBag), request.BagId);

                vm.Bag = _mapper.Map<ShoppingBagByIdDto>(bag);

                return vm;
            }
        }
    }
}
