namespace FineApi.Service.Abstractions;

public interface IEmailSenderService
{
    Task SendEmail();
}