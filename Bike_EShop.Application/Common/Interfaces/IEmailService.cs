using System.Threading.Tasks;

namespace Bike_EShop.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string name, string email, string subject, string message);
    }
}