using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bike_EShop.Web.Models.Product
{
    public class ProductDetailViewModelValidator: AbstractValidator<ProductDetailViewModel>
    {
        public ProductDetailViewModelValidator()
        {
            RuleFor(s => s.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Quantity can't be negative")
                .NotNull().WithMessage("Quantity is required");
        }
    }
}
