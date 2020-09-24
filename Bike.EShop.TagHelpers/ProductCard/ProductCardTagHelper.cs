using Bike_EShop.TagHelpers.NameConsts;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bike_EShop.TagHelpers.ProductCard
{
    [HtmlTargetElement(TagHelperNames.ProductCart)]
    public class ProductCardTagHelper :TagHelper
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public decimal Price { get; set; }
        public int BikeNr { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            output.TagName = "img";
            output.Content.SetHtmlContent(

                "<div class=\"col-lg-3 col-md-4 col-6 mb-3\">" +
                "<div class=\"card\">" +
                $"<img class=\"card-img-top\" src=\"./images/bikes/bike{BikeNr}.png\" alt=\"Bike Photo\">" +
                "<div class=\"card-body text-center\">" +
                $"<h5 class=\"card-title\">{Name.ToUpper()}</h5>" +
                $"<p class=\"card-text\">{Price.ToString("C")}</p>" +
                $"<a href=\"/Product/Detail/{Id}/{BikeNr}\" class=\"btn btn-product\">Add to Cart</a>" +
                "</div>" +
                "</div>" +
                "</div>"
            );
        }
    }
}
