using System;
using System.Collections.Generic;
using System.Text;

namespace Bike_EShop.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public ICollection<ShoppingBag> Bags { get; set; }
    }
}
