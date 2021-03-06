﻿using Bike_EShop.TagHelpers.NameConsts;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bike_EShop.TagHelpers.BikeImage
{
    [HtmlTargetElement(TagHelperNames.BikeImgTagHelper)]
    public class BikeImageTagHelper: TagHelper
    {
        public int BikeId { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            output.TagName = "img";
            output.Content.SetHtmlContent(
                $"<img src=\"../images/bikes/bike{BikeId}.png\" alt=\"Image of a bike\"/>"
            );
        }
    }
}
