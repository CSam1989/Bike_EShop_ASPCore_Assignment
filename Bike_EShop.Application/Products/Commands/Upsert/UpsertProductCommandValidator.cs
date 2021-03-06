﻿using FluentValidation;
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
                .NotNull().WithMessage("Price is required");

            RuleFor(p => p.BikeRegistrationNumber)
                .Length(8).WithMessage("Registrationnumber exactly 8 characters")
                .NotEmpty().WithMessage("Registrationnumber is required")
                .Must(number => number != null && number.ToUpper().StartsWith("ABC")).WithMessage("Registrationnumber starts with ABC");
        }
    }
}
