using System;
using System.Collections.Generic;
using System.Text;
using Bike_EShop.Application.Common.Mappings;
using Bike_EShop.Domain.Entities;

namespace Bike_EShop.Application.Customers.Queries.GetCustomerByUserId
{
    public class CustomerByUserIdDto: IMapFrom<Customer>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string UserId { get; set; }
    }
}
