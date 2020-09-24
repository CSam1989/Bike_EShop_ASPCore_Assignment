using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bike_EShop.Application.ShoppingItems.Commands.Create
{
    public class UpsertShoppingItemCommand: IRequest<int>
    {
        public int ProductId { get; set; }
        public int BagId { get; set; }
        public int Quantity { get; set; }

        public class UpsertShoppingItemCommandHandler : IRequestHandler<UpsertShoppingItemCommand, int>
        {
            private readonly IApplicationDbContext _context;

            public UpsertShoppingItemCommandHandler(IApplicationDbContext context)
            {
                this._context = context;
            }

            public async Task<int> Handle(UpsertShoppingItemCommand request, CancellationToken cancellationToken)
            {
                ShoppingItem entity = await _context.ShoppingItems
                    .FirstOrDefaultAsync(s => s.ProductId == request.ProductId && s.ShoppingBagId == request.BagId);

                if(entity is null)
                {
                    entity = new ShoppingItem
                    {
                        ProductId = request.ProductId,
                        ShoppingBagId = request.BagId,
                        Quantity = request.Quantity
                    };
                    _context.ShoppingItems.Add(entity);
                }
                else
                    entity.Quantity += request.Quantity;

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
