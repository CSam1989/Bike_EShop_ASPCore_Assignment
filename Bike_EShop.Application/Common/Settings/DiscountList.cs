namespace Bike_EShop.Application.Common.Settings
{
    public class DiscountList
    {
        public Discount[] Discounts { get; set; }
    }

    public class Discount
    {
        public int ItemCount { get; set; }
        public double Percentage { get; set; }
    }
}
