using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bike_EShop.Application.Common.Exceptions;
using Bike_EShop.Application.Common.Models;
using Bike_EShop.Application.Products.Commands.Create;
using Bike_EShop.Application.Products.Commands.Delete;
using Bike_EShop.Application.Products.Queries.GetProducts;
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

        // GET: Product/Detail/:id
        public IActionResult Detail()
        {
            return View();
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

        [HttpGet("Admin/Product/Create")]
        // GET: Product/Create
        public IActionResult Create()
        {
            return View(new CreateProductCommand());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSave([Bind("Name,Price")] CreateProductCommand command)
        {
            if (ModelState.IsValid)
            {
                var productId = await Mediator.Send(command);
                return RedirectToAction(nameof(AdminIndex), productId);
            }

            return View(nameof(Create), command);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteProductCommand { Id = id });
                return RedirectToAction(nameof(AdminIndex));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
