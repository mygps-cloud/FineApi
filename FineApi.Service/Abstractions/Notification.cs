namespace FineApi.Service.Abstractions;

public abstract partial class Notification
{
    public string Subject { get; protected set; } = "ჯარიმა";
    public string Username { get; protected set; } = "";
    public string Password { get; protected set; } = "";
    public string SmsAdvertisement  { get;} = "https://api.smsreklama.ge/sms/send/?";
    protected string CombineToAdvertisementSms(string text,string username,string password)=>
        $"username={username}&password={password}&numbers=591144849&text={text}&unicode=1";

    protected string CombineToEmailSms(string body) =>
        $"MyGps გატყობინებთ: გთხოვთ დროულად დაფაროთ თქვენს კუთვნილებაში მყოფი ავტომობილის ჯარიმა {body}.";

}