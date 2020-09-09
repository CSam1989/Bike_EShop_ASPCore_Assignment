using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bike_EShop.Application.Products.Commands.Create
{
    public class CreateProductCommand: IRequest<int>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
        {
            private readonly IApplicationDbContext _context;

            public CreateProductCommandHandler(IApplicationDbContext context)
            {
                this._context = context;
            }
            public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var entity = new Product
                {
                    Name = request.Name.Trim(),
                    Price = request.Price
                };

                _context.Products.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
