using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Application.Customers.Queries.GetCustomerByUserId;
using Bike_EShop.Application.Shoppingbags.Queries.GetBagById;
using Bike_EShop.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bike_EShop.Application.Shoppingbags.Commands.SimulateOrder
{
    [ExcludeFromCodeCoverage] //Its a simulation => no unit testing needed
    public class SimulateOrderCommand: IRequest
    {
        public class SimulateOrderCommandHandler: IRequestHandler<SimulateOrderCommand>
        {
            private readonly IBagSessionService _bagSession;
            private readonly IEmailService _email;
            private readonly IMediator _mediator;
            private readonly UserManager<ApplicationUser> _userManager;

            public SimulateOrderCommandHandler(
                IBagSessionService bagSession, 
                IEmailService email, 
                IMediator mediator, 
                UserManager<ApplicationUser> userManager)
            {
                _bagSession = bagSession;
                _email = email;
                _mediator = mediator;
                _userManager = userManager;
            }

            public async Task<Unit> Handle(SimulateOrderCommand request, CancellationToken cancellationToken)
            {
                // Simulates ordering the shoppingbag but isnt supported by db
                var query = await _mediator.Send(new GetShoppingBagByIdQuery()
                {
                    BagId = await _bagSession.RetrieveBagIdFromSession()
                }, cancellationToken);

                var emailMessage = new StringBuilder();
                emailMessage.Append($"<p>Dear {query.Bag.Customer.FirstName} {query.Bag.Customer.Name},</p>");
                emailMessage.Append($"<p>Thank you for ordering at Sam's Bikeshop. We are processing your order.</p>");
                emailMessage.Append($"<p>Order summary:</p>");

                foreach (var item in query.Bag.Items)
                    emailMessage.Append(
                        $"<p>{item.Product.Name}: {item.Product.Price.ToString("C")} x {item.Quantity} = {item.ItemSubTotal.ToString("C")}</p>");

                if (query.Bag.Discount > 0)
                {
                    emailMessage.Append($"<p>Subtotal: {query.Bag.SubTotal.ToString("C")}</p>");
                    emailMessage.Append($"<p>Discount: {query.Bag.Discount.ToString("C")}</p>");
                }
                emailMessage.Append($"<p>Total Price: {query.Bag.TotalPrice.ToString("C")}</p>");
                emailMessage.Append($"<p>Kind regards.<br/>Sam's Bikeshop</p>");

                var user = await _userManager.FindByIdAsync(query.Bag.Customer.UserId);

                await  _email.SendEmailAsync(
                    $"{query.Bag.Customer.FirstName} {query.Bag.Customer.Name}",
                    user.Email, 
                    subject: "Order confirmation",
                    emailMessage.ToString());

                _bagSession.ClearBag();

                return Unit.Value;
            }
        }
    }
}
