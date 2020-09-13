using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Application.Shoppingbags.Commands.Create;
using Bike_EShop.Web.Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bike_EShop.Application.Customers.Queries.GetCustomerByUserId;

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
            if (BagIdExistsInSession())
                return int.Parse(_getBadIdfromSession());

            var bagId = await _createShoppingBag();

            _accessor.HttpContext.Session.SetObjectAsJson("bagId", bagId);

            return int.Parse(bagId);
        }

        public bool BagIdExistsInSession()
        {
            return !(_getBadIdfromSession() is null);
        }

        public void ClearBag()
        {
            _accessor.HttpContext.Session.Clear();
        }

        private string _getBadIdfromSession()
        {
            return _accessor.HttpContext.Session.GetObjectFromJson<string>("bagId");
        }

        private async Task<string> _createShoppingBag()
        {
            var customer = await _mediator.Send(new GetCustomerByUserIdQuery
            {
                UserId = _accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value
            });

            var id = await _mediator.Send(new CreateShoppingBagCommand
            {
                CustomerId = customer.Customer.Id
            });

            return id.ToString();
        }
    }
}
