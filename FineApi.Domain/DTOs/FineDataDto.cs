namespace FineApi.Domain.DTOs;

public record FineDataDto(string ReceiptNumber,string Date,string Article,decimal Amount,bool Paid);