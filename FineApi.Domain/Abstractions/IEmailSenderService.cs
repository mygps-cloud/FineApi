namespace FineApi.Domain.Abstractions;

public interface IEmailSenderService
{
    Task SendEmail();
}