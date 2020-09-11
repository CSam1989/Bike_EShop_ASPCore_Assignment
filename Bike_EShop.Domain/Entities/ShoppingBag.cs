using System;
using System.Collections.Generic;
using System.Text;

namespace Bike_EShop.Domain.Entities
{
    public class ShoppingBag
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<ShoppingItem> Items { get; set; }
    }
}
