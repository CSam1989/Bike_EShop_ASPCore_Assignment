using Bike_EShop.Application.Products.Queries.GetProductById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bike_EShop.Web.Models.Product
{
    public class ProductDetailViewModel
    {
        public ProductByIdDto Product { get; set; }
        public int BikeNr { get; set; }
        public int Quantity { get; set; }
    }
}
