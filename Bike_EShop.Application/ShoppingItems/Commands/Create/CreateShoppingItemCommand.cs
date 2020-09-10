using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bike_EShop.Application.ShoppingItems.Commands.Create
{
    public class CreateShoppingItemCommand: IRequest<int>
    {
        public int ProductId { get; set; }
        public int BagId { get; set; }
        public int Quantity { get; set; }

        public class CreateShoppingBagCommandHandler : IRequestHandler<CreateShoppingItemCommand, int>
        {
            private readonly IApplicationDbContext _context;

            public CreateShoppingBagCommandHandler(IApplicationDbContext context)
            {
                this._context = context;
            }

            public async Task<int> Handle(CreateShoppingItemCommand request, CancellationToken cancellationToken)
            {
                var entity = new ShoppingItem
                {
                    ProductId = request.ProductId,
                    ShoppingBagId = request.BagId,
                    Quantity = request.Quantity
                };

                _context.ShoppingItems.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
