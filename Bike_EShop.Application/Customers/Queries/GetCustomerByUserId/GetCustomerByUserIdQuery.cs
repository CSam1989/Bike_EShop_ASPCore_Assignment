using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Bike_EShop.Application.Common.Exceptions;
using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bike_EShop.Application.Customers.Queries.GetCustomerByUserId
{
    public class GetCustomerByUserIdQuery: IRequest<CustomerByUserIdVM>
    {
        public string UserId { get; set; }

        public class GetCustomerByUserIdQueryHandler : IRequestHandler<GetCustomerByUserIdQuery, CustomerByUserIdVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCustomerByUserIdQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<CustomerByUserIdVM> Handle(GetCustomerByUserIdQuery request, CancellationToken cancellationToken)
            {
                var vm = new CustomerByUserIdVM();

                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.UserId == request.UserId, cancellationToken);

                if(customer is null)
                    throw new NotFoundException(nameof(Customer), request.UserId);

                vm.Customer = _mapper.Map<CustomerByUserIdDto>(customer);

                return vm;
            }
        }
    }
}
