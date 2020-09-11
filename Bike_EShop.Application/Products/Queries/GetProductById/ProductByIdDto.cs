using Bike_EShop.Application.Common.Mappings;
using Bike_EShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bike_EShop.Application.Products.Queries.GetProductById
{
    public class ProductByIdDto: IMapFrom<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
