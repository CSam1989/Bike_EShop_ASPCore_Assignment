using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bike_EShop.Application.ShoppingItems.Commands.Create
{
    public class CreateShoppingItemCommandValidator: AbstractValidator<CreateShoppingItemCommand>
    {
        public CreateShoppingItemCommandValidator()
        {
            RuleFor(s => s.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Quantity can't be negative")
                .NotNull().WithMessage("Quantity is required");
        }
    }
}
