namespace FineApi.Domain.Abstractions;
public interface IReceivedSmsService
{
    Task UpdateReceivedSms(string receiptNumber, bool paid);
}
