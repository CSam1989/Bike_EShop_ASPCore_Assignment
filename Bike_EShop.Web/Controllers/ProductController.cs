using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Bike_EShop.Application.Common.Extensions;
using Bike_EShop.Application.Products.Commands.Delete;
using Bike_EShop.Application.Products.Commands.Upsert;
using Bike_EShop.Application.Products.Queries.GetProductById;
using Bike_EShop.Application.Products.Queries.GetProducts;
using Bike_EShop.Application.ShoppingItems.Commands.Create;
using Bike_EShop.Domain.Entities;
using Bike_EShop.Web.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace Bike_EShop.Web.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Product
        public async Task<IActionResult> Index(int? currentPage)
        {
            return View(await Mediator.Send(new GetProductsQuery()
            {
                CurrentPage = currentPage
            }));
        }

        // GET: Product/Detail/1
        [HttpGet("Product/Detail/{id}/{bikeNr}")]
        public async Task<IActionResult> Detail(int id, int bikeNr)
        {
            var query = await Mediator.Send(new GetProductByIdQuery { Id = id });

            return View(new ProductDetailViewModel 
            { 
                Product = query.Product,
                Quantity = 1,
                BikeNr = bikeNr
            });
        }

        // GET: Admin/Product
        [HttpGet("Admin/Product")]
        public async Task<IActionResult> AdminIndex(int? currentPage)
        {
            return View(await Mediator.Send(new GetProductsQuery()
            {
                CurrentPage = currentPage
            }));
        }

        // GET: Admin/Product/Create
        [HttpGet("Admin/Product/Create")]
        public IActionResult Create()
        {
            return View(new UpsertProductCommand());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveCreate([Bind("Name,Price")] UpsertProductCommand command)
        {
            if (ModelState.IsValid)
            {
                await Mediator.Send(command);
                return RedirectToAction(nameof(AdminIndex));
            }

            return View(nameof(Create), command);
        }
        
        // GET: Admin/Product/Update/1
        [HttpGet("Admin/Product/Update/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var query = await Mediator.Send(new GetProductByIdQuery { Id = id });
            return View(new UpsertProductCommand
            {
                Id = query.Product.Id,
                Name = query.Product.Name,
                Price = query.Product.Price
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveUpdate(int id, [Bind("Id,Name,Price")] UpsertProductCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var productId = await Mediator.Send(command);
                return RedirectToAction(nameof(AdminIndex), productId);
            }

            return View(nameof(Update), command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteProductCommand { Id = id });
            return RedirectToAction(nameof(AdminIndex));
        }
    }
}
