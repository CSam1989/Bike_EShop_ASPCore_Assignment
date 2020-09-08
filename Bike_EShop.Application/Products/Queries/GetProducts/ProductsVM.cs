using System;
using System.Collections.Generic;
using System.Text;

namespace Bike_EShop.Application.Products.Queries.GetProducts
{
    public class ProductsVM
    {
        public IEnumerable<ProductsDto> List { get; set; }
    }
}
