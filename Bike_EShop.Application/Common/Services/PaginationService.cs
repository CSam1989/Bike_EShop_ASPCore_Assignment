using Bike_EShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bike_EShop.Application.Common.Services
{
    public class PaginationService<T> : IPaginationService<T> 
        where T : class
    {
        public IQueryable<T> Paginate(IQueryable<T> query, int currentPage, int pageSize)
        {
            return query
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
