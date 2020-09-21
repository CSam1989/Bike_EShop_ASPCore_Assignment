using Bike_EShop.Domain.Entities;

namespace Bike_EShop.Application.Common.Interfaces
{
    public interface IDiscountService
    {
        decimal Calculate(ShoppingBag bag);
    }
}