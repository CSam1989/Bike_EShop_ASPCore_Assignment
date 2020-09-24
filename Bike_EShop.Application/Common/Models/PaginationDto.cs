using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Bike_EShop.Application.Common.Models
{
    [ExcludeFromCodeCoverage]
    public class PaginationDto
    {
        public int? CurrentPage { get; set; }
        public int PageSize { get; set; } = 10;
    }
}
