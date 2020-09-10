using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Application.Shoppingbags.Commands.Create;
using Bike_EShop.Application.ShoppingItems.Commands.Create;
using Bike_EShop.Web.Common.Extensions;
using Bike_EShop.Web.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace Bike_EShop.Web.Controllers
{
    public class ShoppingListController : BaseController
    {
        private readonly IBagSessionService _bagSession;

        public ShoppingListController(IBagSessionService bagSession)
        {
            this._bagSession = bagSession;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToBag([FromForm] ProductDetailViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await Mediator.Send(new CreateShoppingItemCommand
                {
                    ProductId = vm.Product.Id,
                    BagId = await _bagSession.RetrieveBagIdFromSession(),
                    Quantity = vm.Quantity
                });
                return RedirectToAction("Index", "Product");
            }

            return View("Product/Detail", vm);
        }
    }
}
