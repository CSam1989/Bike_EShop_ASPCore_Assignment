using System;
using System.Collections.Generic;
using System.Text;
using Bike_EShop.Application.Common.Interfaces;
using Bike_EShop.Application.Common.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Bike_EShop.Application.Common.Factories
{
    public class MailFactory : IMailFactory
    {
        private readonly EmailSettings _settings;

        public MailFactory(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }
        public MimeMessage CreateMailMessage(string name, string email, string subject, string message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
            mimeMessage.To.Add(new MailboxAddress(name, email));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart("html")
            {
                Text = message
            };

            return mimeMessage;
        }

        public SmtpClient CreateMailClient()
        {
            return new SmtpClient();
        }
    }
}
