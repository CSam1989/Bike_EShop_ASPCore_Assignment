using Bike_EShop.Application.Common.Mappings;
using Bike_EShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bike_EShop.Application.Shoppingbags.Queries.GetBagById
{
    public class ProductShoppingItemsShoppingBagByIdDto: IMapFrom<Product>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
