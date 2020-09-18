using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Domain.Entities;
using MediatR;

namespace Bike_EShop.Application.Customers.Commands.Create
{
    public class CreateCustomerCommand: IRequest<int>
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string UserId { get; set; }

        public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
        {
            private readonly IApplicationDbContext _context;

            public CreateCustomerCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                var entity = new Customer
                {
                    FirstName = request.FirstName.Trim(),
                    Name = request.Name.Trim(),
                    UserId = request.UserId.Trim()
                };

                _context.Customers.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
