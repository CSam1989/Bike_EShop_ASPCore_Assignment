using Bike_EShop.Application.Common.Extensions;
using Bike_EShop.Application.Common.Mappings;
using Bike_EShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bike_EShop.Application.Shoppingbags.Queries.GetBagById
{
    public class ShoppingBagByIdDto: IMapFrom<ShoppingBag>
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public CustomerShoppingBagByIdDto Customer { get; set; }
        public IEnumerable<ShoppingItemsShoppingBagByIdDto> Items { get; set; }
        public decimal TotalPrice => Items.CalculateTotalPrice();

    }
}
