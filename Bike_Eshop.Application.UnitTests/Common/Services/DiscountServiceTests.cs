using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bike_EShop.Application.Common.Services;
using Bike_EShop.Application.Common.Settings;
using Bike_EShop.Domain.Entities;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace Bike_Eshop.Application.UnitTests.Common.Services
{
    [TestFixture]
    public class DiscountServiceTests
    {
        private DiscountService _discount;
        private ShoppingBag _shoppingBag;

        [SetUp]
        public void Setup()
        {
            //no need for mocking
            var discountList = new DiscountList
            {
                Discounts = new[]
                {
                    new Discount() {ItemCount = 10, Percentage = 10},
                    new Discount() {ItemCount = 20, Percentage = 20}
                }
            };

            _discount = new DiscountService(discountList);

            _shoppingBag = new ShoppingBag()
            {
                Items = new List<ShoppingItem>()
            };
        }

        [TestCase(100,5, 0)]
        [TestCase(100,10, 100)]
        [TestCase(100,20, 400)]
        public void WhenCalculatingDiscount_Given10items_ShouldReturn10PercentDiscount(decimal itemprice, int itemcount, decimal discount)
        {
            _shoppingBag.Items.Add(new ShoppingItem()
            {
                Product = new Product()
                {
                    Price = itemprice,
                },
                Quantity = itemcount
            });

            var result = _discount.Calculate(_shoppingBag);

            result.ShouldBe(discount);
        }
    }
}
