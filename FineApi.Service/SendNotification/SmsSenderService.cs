using System.Net;
using System.Net.Mail;
using System.Text;
using FineApi.Service.Abstractions;

namespace FineApi.Service.SendNotification;

public class SmsSenderService:Notification,ISmsSenderService
{
    public async Task<bool> SendSmsToNumber(string text)
    {
        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        
        Username = "Mygps.ge";
        Password = "m%24%24g&brand=2";
        
        var request = (HttpWebRequest) WebRequest.Create(SmsAdvertisement);
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";

        var postDataBytes = Encoding.UTF8.GetBytes(CombineToAdvertisementSms(text,Username,Password));
        request.ContentLength = postDataBytes.Length;
        await using (var stream = request.GetRequestStream())
        {
            await stream.WriteAsync(postDataBytes);
        }

        var response = (HttpWebResponse) request.GetResponse();
        var responseString = await new StreamReader(response.GetResponseStream()).ReadToEndAsync();
        return true;
    }
    
    public async Task<bool> SendSmsToEmail(string reciver,List<(string, string,string)> carNumbersAndReceiptNumber)
    {
        StringBuilder result = new StringBuilder();
        foreach (var car in carNumbersAndReceiptNumber)
        {
            result.Append("მანქანა:"+car.Item1);
            result.Append(", ქვითრის ნომერი: "+car.Item2);
            result.AppendLine(", თანხა: " + car.Item3);
            result.AppendLine();
        }
        
        Username = "noreply3@mygps.ge";
        Password = "sgpzhupqepdtcpob";
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(Username);
        mail.Sender = new MailAddress(Username);
        mail.To.Add(reciver);
        mail.IsBodyHtml = true;
        mail.Subject = Subject;
        mail.Body = CombineToEmailSms(result.ToString());

        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
        smtp.UseDefaultCredentials = false;

        smtp.Credentials = new System.Net.NetworkCredential(Username, Password);
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.EnableSsl = true;
            
        smtp.Send(mail);
        return true;
    }
}