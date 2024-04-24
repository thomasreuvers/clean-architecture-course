using CleanArchitecture.Application.Contracts.Email;
using CleanArchitecture.Application.Models.Email;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CleanArchitecture.Infrastructure.EmailService;

public class EmailSender(IOptions<EmailSettings> emailSettings) : IEmailSender
{
    public async Task<bool> SendEmail(EmailMessage email)
    {
        var client = new SendGridClient(emailSettings.Value.ApiKey);
        var to = new EmailAddress(email.To);
        var from = new EmailAddress
        {
            Email = emailSettings.Value.FromAddress,
            Name = emailSettings.Value.FromName
        };

        var message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);
        var response = await client.SendEmailAsync(message);

        return response.IsSuccessStatusCode;
    }
}