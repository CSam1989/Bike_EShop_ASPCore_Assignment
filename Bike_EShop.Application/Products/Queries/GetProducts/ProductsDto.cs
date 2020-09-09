using Bike_EShop.Application.Common.Mappings;
using Bike_EShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bike_EShop.Application.Products.Queries.GetProducts
{
    public class ProductsDto: IMapFrom<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int BikeNr { get; set; } //Random nummer tussen 1 & 4 zodat er een random foto van een bike verschijnt
    }
}
