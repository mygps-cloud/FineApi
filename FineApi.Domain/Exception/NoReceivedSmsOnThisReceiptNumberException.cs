namespace FineApi.Service.Exception;

public class NoReceivedSmsOnThisReceiptNumberException:System.Exception
{
    public NoReceivedSmsOnThisReceiptNumberException() : base("No Sms Received On This Receipt Number") { }
}
