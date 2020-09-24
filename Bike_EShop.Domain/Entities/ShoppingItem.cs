using System;
using System.Collections.Generic;
using System.Text;

namespace Bike_EShop.Domain.Entities
{
    public class ShoppingItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int ShoppingBagId { get; set; }
        public ShoppingBag Bag { get; set; }

    }
}
