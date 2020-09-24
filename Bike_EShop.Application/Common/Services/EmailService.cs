using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Application.Common.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Bike_EShop.Application.Common.Services
{
    //This is an external service, so its not a unit test
    [ExcludeFromCodeCoverage]
    public class EmailService: IEmailService
    {
        private readonly IMailFactory _mailFactory;
        private readonly ILogger<EmailService> _logger;
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> settings, IMailFactory mailFactory, ILogger<EmailService> logger)
        {
            _mailFactory = mailFactory;
            _logger = logger;
            _settings = settings.Value;
        }

        public async Task SendEmailAsync(string name, string email, string subject, string message)
        {
            try
            {
                using var client = _mailFactory.CreateMailClient();
                var mailMessage = _mailFactory.CreateMailMessage(name, email, subject, message);

                await client.ConnectAsync(_settings.Server, _settings.Port, false);
                await client.SendAsync(mailMessage);
                await client.DisconnectAsync(true);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e, $"unexpected error occurred when trying to send an email to {email}");
                throw;
            }
            
        }
    }
}