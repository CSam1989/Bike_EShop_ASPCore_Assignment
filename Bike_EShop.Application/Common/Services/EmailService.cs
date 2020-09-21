using System.Threading.Tasks;
using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Application.Common.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Bike_EShop.Application.Common.Services
{
    public class EmailService: IEmailService
    {
        private readonly IMailFactory _mailFactory;
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> settings, IMailFactory mailFactory)
        {
            _mailFactory = mailFactory;
            _settings = settings.Value;
        }

        public async Task SendEmailAsync(string name, string email, string subject, string message)
        {
            using var client = _mailFactory.CreateMailClient();
            var mailMessage = _mailFactory.CreateMailMessage(name, email, subject, message);

            await client.ConnectAsync(_settings.Server, _settings.Port, false);
            await client.SendAsync(mailMessage);
            await client.DisconnectAsync(true);
        }
    }
}