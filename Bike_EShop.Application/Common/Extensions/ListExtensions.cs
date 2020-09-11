using Bike_EShop.Application.Shoppingbags.Queries.GetBagById;
using Bike_EShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bike_EShop.Application.Common.Extensions
{
    public static class ListExtensions
    {
        public static decimal CalculateTotalPrice(this IEnumerable<ShoppingItemsShoppingBagByIdDto> items)
        {
            decimal totalPrice = 0M;
            foreach (var item in items)
            {
                totalPrice += item.ItemSubTotal;
            }
            return totalPrice;
        }
    }
}
