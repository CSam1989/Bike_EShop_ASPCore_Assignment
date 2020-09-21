using System;
using System.Collections.Generic;
using System.Text;
using Bike_EShop.Application.Common.Interfaces;
using System.Linq;
using FluentValidation;

namespace Bike_EShop.Application.Customers.Commands.Create
{
    public class CreateCustomerCommandValidator :AbstractValidator<CreateCustomerCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateCustomerCommandValidator(IApplicationDbContext context)
        {
            _context = context;
            RuleFor(c => c.FirstName)
                .MaximumLength(50).WithMessage("Firstname max 50 characters");

            RuleFor(c => c.Name)
                .MaximumLength(50).WithMessage("Name max 50 characters")
                .NotEmpty().WithMessage("Name is required");

            RuleFor(c => c)
                .Must(c => !IsCustomerUnique(c))
                .WithMessage("Voornaam en/of Naam moet uniek zijn");
        }

        private bool IsCustomerUnique(CreateCustomerCommand c)
        {
            if (c.FirstName is null)
                return _context.Customers.Any(customer =>
                    customer.Name.ToLower() == c.Name.Trim().ToLower());

            return _context.Customers.Any(customer =>
               customer.Name.ToLower() == c.Name.Trim().ToLower() &&
                customer.FirstName.ToLower() == c.FirstName.Trim().ToLower());
        }
    }
}
