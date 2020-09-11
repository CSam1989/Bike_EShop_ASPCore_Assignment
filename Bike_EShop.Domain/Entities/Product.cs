using System;
using System.Collections.Generic;
using System.Text;

namespace Bike_EShop.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ICollection<ShoppingItem> ShoppingItems { get; set; }
    }
}
