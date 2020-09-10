using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bike_EShop.Application.Shoppingbags.Commands.Create
{
    public class CreateShoppingBagCommand: IRequest<int>
    {
        public int CustomerId { get; set; }
        public class CreateShoppingBagCommandHandler : IRequestHandler<CreateShoppingBagCommand, int>
        {
            private readonly IApplicationDbContext _context;
            private readonly IDateTime _dateTime;

            public CreateShoppingBagCommandHandler(IApplicationDbContext context, IDateTime dateTime)
            {
                this._context = context;
                this._dateTime = dateTime;
            }

            public async Task<int> Handle(CreateShoppingBagCommand request, CancellationToken cancellationToken)
            {
                var entity = new ShoppingBag
                {
                    CustomerId = request.CustomerId,
                    Date = _dateTime.Now
                };

                _context.ShoppingBags.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
