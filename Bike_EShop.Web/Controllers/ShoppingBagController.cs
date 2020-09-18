using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Application.Shoppingbags.Queries.GetBagById;
using Microsoft.AspNetCore.Mvc;

namespace Bike_EShop.Web.Controllers
{
    public class ShoppingBagController : BaseController
    {
        private readonly IBagSessionService _bagSession;

        public ShoppingBagController(IBagSessionService bagSession)
        {
            this._bagSession = bagSession;
        }

        public async Task<IActionResult> Index()
        {
            if(_bagSession.BagIdExistsInSession())
                return View(await Mediator.Send(new GetShoppingBagByIdQuery 
                { 
                    BagId = await _bagSession.RetrieveBagIdFromSession()
                }));
            return View();
        }

        public IActionResult Order()
        {
            //Simulates ordering the shoppingbag but isnt supported by db
            _bagSession.ClearBag();
            return RedirectToAction("Index", "Product");
        }
    }
}
