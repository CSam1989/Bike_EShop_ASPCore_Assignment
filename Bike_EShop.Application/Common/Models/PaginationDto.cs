using System;
using System.Collections.Generic;
using System.Text;

namespace Bike_EShop.Application.Common.Models
{
    public class PaginationDto
    {
        public int? CurrentPage { get; set; }
        public int PageSize { get; set; } = 10;
    }
}
