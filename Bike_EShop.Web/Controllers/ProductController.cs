using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bike_EShop.Application.Products.Queries.GetProducts;
using Microsoft.AspNetCore.Mvc;

namespace Bike_EShop.Web.Controllers
{
    public class ProductController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            return View(await Mediator.Send(new GetProductsQuery()));
        }

        public IActionResult Detail()
        {
            return View();
        }
    }
}
