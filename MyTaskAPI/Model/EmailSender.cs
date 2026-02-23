using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace MyTaskAPI.Model
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            _logger = logger;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            throw new Exception("Null SendGridKey");
         
        }

        public async Task Execute(string apiKey, string subject, string message, string toEmail)
        {
            
            _logger.LogInformation($"Failure Email to {toEmail}");
        }
    }
}
