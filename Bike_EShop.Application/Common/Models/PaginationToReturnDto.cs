using System;
using System.Collections.Generic;
using System.Text;

namespace Bike_EShop.Application.Common.Models
{
    public class PaginationToReturnDto: PaginationDto
    {
        public int Count { get; set; }
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;
    }
}
