using Bike_EShop.Application.Common.Exceptions;
using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bike_EShop.Application.Products.Commands.Upsert
{
    public class UpsertProductCommand: IRequest<int>
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string BikeRegistrationNumber { get; set; }

        public class UpsertProductCommandHandler : IRequestHandler<UpsertProductCommand, int>
        {
            private readonly IApplicationDbContext _context;

            public UpsertProductCommandHandler(IApplicationDbContext context)
            {
                this._context = context;
            }
            public async Task<int> Handle(UpsertProductCommand request, CancellationToken cancellationToken)
            {
                Product entity;

                if (request.Id.HasValue)
                {
                    entity = await _context.Products.FindAsync(new object[] { request.Id.Value }, cancellationToken);

                    if (entity is null)
                        throw new NotFoundException(nameof(Product), request.Id.Value);
                }
                else
                {
                    entity = new Product();
                    _context.Products.Add(entity);
                }

                entity.Name = request.Name.Trim();
                entity.Price = request.Price;
                entity.BikeRegistrationNumber = request.BikeRegistrationNumber.ToUpper().Trim();

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
