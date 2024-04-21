using FineApi.Domain.Enums;
using FineApi.Service.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FineApi.Service;

internal class NotificationServiceManager:INotificationServiceManager
{
    private readonly ISmsSenderService _smsSenderService;
    private readonly IUnitOfWorkRepository _unitOfWorkRepository;

    public NotificationServiceManager(IUnitOfWorkRepository unitOfWorkRepository, ISmsSenderService smsSenderService)
    {
        _unitOfWorkRepository = unitOfWorkRepository;
        _smsSenderService = smsSenderService;
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
                {
                    await _smsSenderService.SendSmsToEmail(item.Email,carNumbersAndReceiptNumber);
                    //await _smsSenderService.SendSmsToNumber()
                }
            }
        }
        
    }
}