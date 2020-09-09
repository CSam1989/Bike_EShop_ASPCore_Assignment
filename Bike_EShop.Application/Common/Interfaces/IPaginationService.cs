using System.Linq;

namespace Bike_EShop.Application.Common.Interfaces
{
    public interface IPaginationService<T> 
        where T : class
    {
        IQueryable<T> Paginate(IQueryable<T> query, int currentPage, int pageSize);
    }
}