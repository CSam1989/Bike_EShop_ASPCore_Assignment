using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bike_EShop.Application.Common.Models;
using Bike_EShop.Application.Products.Commands.Create;
using Bike_EShop.Application.Products.Queries.GetProducts;
using Microsoft.AspNetCore.Mvc;

namespace Bike_EShop.Web.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Products
        public async Task<IActionResult> Index(int? currentPage)
        {
            return View(await Mediator.Send(new GetProductsQuery()
            {
                CurrentPage = currentPage
            }));
        }

        // GET: Products/Detail/:id
        public IActionResult Detail()
        {
            return View();
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price")] CreateProductCommand command)
        {
            if (ModelState.IsValid)
            {
                var productId = await Mediator.Send(command);
                return RedirectToAction(nameof(Index), productId);
            }

            return View(command);
        }
    }
}
