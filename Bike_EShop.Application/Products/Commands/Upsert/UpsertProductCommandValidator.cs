using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bike_EShop.Application.Products.Commands.Upsert
{
    public class UpsertProductCommandValidator: AbstractValidator<UpsertProductCommand>
    {
        public UpsertProductCommandValidator()
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
