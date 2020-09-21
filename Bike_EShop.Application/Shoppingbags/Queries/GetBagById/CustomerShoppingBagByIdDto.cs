using System;
using System.Collections.Generic;
using System.Text;
using Bike_EShop.Application.Common.Mappings;
using Bike_EShop.Domain.Entities;

namespace Bike_EShop.Application.Shoppingbags.Queries.GetBagById
{
    public class CustomerShoppingBagByIdDto:IMapFrom<Customer>
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string UserId { get; set; }
    }
}
