namespace FineApi.Domain.DTOs;

public record FineDataDto(string Date, string Article, int? Amount, bool Paid)
{
    public string ReceiptNumber { get; set; }
};