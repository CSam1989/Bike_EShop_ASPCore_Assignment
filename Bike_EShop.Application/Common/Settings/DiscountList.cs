using System.Diagnostics.CodeAnalysis;

namespace Bike_EShop.Application.Common.Settings
{
    [ExcludeFromCodeCoverage]
    public class DiscountList
    {
        public Discount[] Discounts { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Discount
    {
        public int ItemCount { get; set; }
        public double Percentage { get; set; }
    }
}
