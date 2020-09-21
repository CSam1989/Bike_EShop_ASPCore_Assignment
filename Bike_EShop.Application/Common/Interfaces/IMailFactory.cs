using MailKit.Net.Smtp;
using MimeKit;

namespace Bike_EShop.Application.Common.Interfaces
{
    public interface IMailFactory
    {
        MimeMessage CreateMailMessage(string name, string email, string subject, string message);
        SmtpClient CreateMailClient();
    }
}