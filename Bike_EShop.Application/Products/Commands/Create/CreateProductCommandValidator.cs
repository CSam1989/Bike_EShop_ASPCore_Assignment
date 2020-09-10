using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bike_EShop.Application.Products.Commands.Create
{
    public class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name)
                .MaximumLength(50).WithMessage("Name max 50 characters")
                .NotEmpty().WithMessage("Name is required");

            RuleFor(p => p.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price can't be negative")
                .NotEmpty().WithMessage("Price is required"); ;
        }
    }
}
