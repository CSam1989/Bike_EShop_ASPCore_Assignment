using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Application.Common.Models;
using Bike_EShop.Domain.Entities;

namespace Bike_EShop.Application.Common.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly DiscountList _list;

        public DiscountService(DiscountList list)
        {
            _list = list;
        }

        public decimal Calculate(ShoppingBag bag)
        {
            var discounts = _list.Discounts.OrderByDescending(d => d.ItemCount);

            var totalItemsInBag = bag.Items.Sum(shoppingItem => shoppingItem.Quantity);
            var totalPrice = bag.Items.Sum(item => item.Quantity * item.Product.Price);

            return (
                from discount 
                    in discounts 
                where totalItemsInBag >= discount.ItemCount 
                select totalPrice * (decimal) (discount.Percentage / 100)).FirstOrDefault();
        }
    }
}
