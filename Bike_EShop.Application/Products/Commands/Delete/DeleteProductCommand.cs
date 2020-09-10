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

namespace Bike_EShop.Application.Products.Commands.Delete
{
    public class DeleteProductCommand:IRequest
    {
        public int Id { get; set; }

        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
        {
            private readonly IApplicationDbContext _context;

            public DeleteProductCommandHandler(IApplicationDbContext context)
            {
                this._context = context;
            }
            public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Products.FindAsync(request.Id, cancellationToken);

                if (entity is null)
                    throw new NotFoundException(nameof(Product), request.Id);

                _context.Products.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
