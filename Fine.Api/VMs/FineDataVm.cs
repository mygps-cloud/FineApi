namespace Fine.Api.VMs;

public record FineDataVm(string ReceiptNumber,string Date,string Article,decimal? Amount,bool Paid);