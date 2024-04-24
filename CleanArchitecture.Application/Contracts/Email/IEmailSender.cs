using CleanArchitecture.Application.Models.Email;

namespace CleanArchitecture.Application.Contracts.Email;

public interface IEmailSender
{
    Task<bool> SendEmail(EmailMessage email);
}