using System.Net.Mail;
using System.Text;
using AutoMapper;
using FineApi.Domain.Abstractions;
using FineApi.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace FineApi.Service;

public class EmailSenderService:IEmailSenderService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWorkRepository _unitOfWorkRepository;

    public EmailSenderService(IMapper mapper, IUnitOfWorkRepository unitOfWorkRepository)
    {
        _mapper = mapper;
        _unitOfWorkRepository = unitOfWorkRepository;
    }

    public async Task SendEmail()
    {
        var receivedSms = _unitOfWorkRepository.ReceivedSmsRepository.Set.Where(s => s.FineStatus==FineStatus.Unpaid);
        
        var User = _unitOfWorkRepository.EmailSenderRepository
            .Set
            .Include(company => company.UserCarInformation) 
            .Select(company => new
            {
                company.Email,
                UserCarInformation = company.UserCarInformation.Select(userCarInfo => new
                {
                    userCarInfo.CarNumber,
                })
            })
            .ToList();

        foreach (var item in User)
        {
            if (item.Email != null)
            {
                List<(string, string,string)> carNumbersAndReceiptNumber = new List<(string, string,string)>();
                foreach (var user in item.UserCarInformation)
                {
                    foreach (var sms in receivedSms)
                    {
                        if (sms.CarNumber.Equals(user.CarNumber))
                        {
                            carNumbersAndReceiptNumber.Add((sms.CarNumber,sms.ReceiptNumber,sms.Amount.ToString()));
                        }
                    }
                }

                if (carNumbersAndReceiptNumber.Any())
                    SendMessage(item.Email, carNumbersAndReceiptNumber);
            }
        }
        
    }

    private void SendMessage(string reciver,List<(string, string,string)> carNumbersAndReceiptNumber)
    {
        StringBuilder result = new StringBuilder();
        foreach (var car in carNumbersAndReceiptNumber)
        {
            result.Append("მანქანა:"+car.Item1);
            result.Append(", ქვითრის ნომერი: "+car.Item2);
            result.AppendLine(", თანხა: " + car.Item3+". ");
            result.AppendLine();
        }

        string finalResult = result.ToString();
        var subject = "ჯარიმა";
        var body=$"MyGps გატყობინებთ:გთხოვთ დროულად დაფაროთ თქვენს კუთვნილებაში მყოფი ავტომობილის ჯარიმა {finalResult}.";
        var username = "noreply3@mygps.ge";
        var pw = "sgpzhupqepdtcpob";
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(username);
        mail.Sender = new MailAddress(username);
        mail.To.Add(reciver);
        mail.IsBodyHtml = true;
        mail.Subject = subject;
        mail.Body = body;

        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
        smtp.UseDefaultCredentials = false;

        smtp.Credentials = new System.Net.NetworkCredential(username, pw);
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.EnableSsl = true;
            
        smtp.Send(mail);
    }
}