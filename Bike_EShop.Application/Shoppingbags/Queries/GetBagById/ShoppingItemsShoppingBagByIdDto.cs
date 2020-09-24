using AutoMapper;
using Bike_EShop.Application.Common.Mappings;
using Bike_EShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bike_EShop.Application.Shoppingbags.Queries.GetBagById
{
    public class ShoppingItemsShoppingBagByIdDto: IMapFrom<ShoppingItem>
    {
        public int Quantity { get; set; }
        public ProductShoppingItemsShoppingBagByIdDto Product { get; set; }

        public decimal ItemSubTotal => Quantity * Product.Price;
    }
}
