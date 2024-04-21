namespace FineApi.Service.Abstractions;

public interface ISmsSenderService
{
    public Task<bool> SendSmsToNumber(string text);
    public Task<bool> SendSmsToEmail(string reciver, List<(string, string, string)> carNumbersAndReceiptNumber);
}