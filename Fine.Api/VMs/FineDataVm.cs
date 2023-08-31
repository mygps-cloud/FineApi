namespace Fine.Api.VMs;

public record FineDataVm(string ReceiptNumber,string Date,string Article,int? Amount,bool Paid);