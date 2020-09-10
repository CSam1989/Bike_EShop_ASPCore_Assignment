using System.Threading.Tasks;

namespace Bike_EShop.Application.Common.Interfaces
{
    public interface IBagSessionService
    {
        Task<int> RetrieveBagIdFromSession();
    }
}