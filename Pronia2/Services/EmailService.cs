using MailKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Pronia2.Abstractions;
using Pronia2.ViewModels.EmailViewModels;


namespace Pronia2.Services
{
    public class EmailService : IEMailService
    {
        private readonly IConfiguration _configuration;
        private readonly SmtpSettingsVm _settings;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _settings = _configuration.GetSection("SmtpSettings").Get<SmtpSettingsVm>() ?? new SmtpSettingsVm();
        }
        public async Task SendEmailAsync(string email, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
            message.To.Add(new MailboxAddress(email,email));

            message.Subject = subject;
            message.Body = new TextPart("html")
            {
                Text = body
            };

            using var client = new SmtpClient();
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            await client.ConnectAsync(_settings.Server, _settings.Port, true);
            await client.AuthenticateAsync(_settings.UserName, _settings.Password);
            await client.SendAsync(message);


        }

        
    }
}
