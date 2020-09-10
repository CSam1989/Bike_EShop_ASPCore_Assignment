using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Application.Shoppingbags.Commands.Create;
using Bike_EShop.Web.Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bike_EShop.Web.Common.Services
{
    public class BagSessionService : IBagSessionService
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _accessor;

        public BagSessionService(IMediator mediator, IHttpContextAccessor accessor)
        {
            this._mediator = mediator;
            this._accessor = accessor;
        }

        public async Task<int> RetrieveBagIdFromSession()
        {
            var bagId = _accessor.HttpContext.Session.GetObjectFromJson<string>("bagId");

            if (bagId is null)
            {
                //CustomerId wordt nog aangepast van zodra Identity Authentication is ingevoerd
                var id = await _mediator.Send(new CreateShoppingBagCommand { CustomerId = 1});
                bagId = id.ToString();
                _accessor.HttpContext.Session.SetObjectAsJson("bagId", bagId);
            }

            return int.Parse(bagId);
        }
    }
}
