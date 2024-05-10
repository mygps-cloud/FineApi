using System.Globalization;
using FineApi.Domain.Enums;
using FineApi.Domain.Models;
using FineApi.Service.Abstractions;
using FineApi.Service.DTOs;

namespace FineApi.Service;
internal class ReceivedSmsService : IReceivedSmsService
{
    private readonly IUnitOfWorkRepository _unitOfWorkRepository;
    public ReceivedSmsService(IUnitOfWorkRepository unitOfWorkRepository)
    {
        _unitOfWorkRepository = unitOfWorkRepository;
    }
    public async Task UpdateReceivedSms(List<FineDataDto> data)
    {
        string recipieLatin;
        string recipie="";
        string carnumber="";
        var userCarInformation = await _unitOfWorkRepository.UserCarInformationRepository.GetAll()!;
        
        /*var fine = await _unitOfWorkRepository.LoggerRepository.
            FirstOrDefaultAsync(x=>x.UserCarInformation.CarNumber==data.First().ReceiptNumber
                .Substring(data.First().ReceiptNumber.Length-7));
        
        if (fine is not null)
        {
            await _unitOfWorkRepository.LoggerRepository.Remove(fine);
            await _unitOfWorkRepository.SaveAsync();
        }*/
        
        foreach (var fineData in data)
        {
            foreach (var carInformation in userCarInformation)
            {
                if (fineData.ReceiptNumber.Contains(carInformation.CarNumber))
                {
                    recipie=fineData.ReceiptNumber.Substring(0,
                        fineData.ReceiptNumber.Length - carInformation.CarNumber.Length);
                    carnumber = carInformation.CarNumber;
                    
                    TransilateToGeorgian(recipie,out recipieLatin);
                    var receivedSms = await _unitOfWorkRepository.ReceivedSmsRepository.FirstOrDefaultAsync(x => x.ReceiptNumber == recipieLatin);
                    if (receivedSms == null)
                    {
                        receivedSms = new ReceivedSms();
                        fineData.ReceiptNumber = recipieLatin;
                        receivedSms.FineStatus = fineData.Paid == true ? Domain.Enums.FineStatus.Paid : Domain.Enums.FineStatus.Unpaid;
                        receivedSms.ReceiptNumber = fineData.ReceiptNumber;
                        receivedSms.Amount = fineData.Amount;
                        receivedSms.Deleted = true;
                        receivedSms.Parsed = true;
                        receivedSms.Sender = "POLICE";
                        receivedSms.Street = "adgilmdebareoba ucnobia";
                        receivedSms.Text =
                            $"tkven kutvnilebashi myofi avtomobili nomrit {carnumber} dajarimda {fineData.Amount}larit qvitris nomeria {recipieLatin} damatebiti informaciistvis ewviet chvens saits";
                        receivedSms.Term = 30;
                        DateTimeOffset? DateOfFine = DateTimeOffset.ParseExact(fineData.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                        if (DateOfFine.HasValue)
                        {
                            DateTimeOffset? LastDateOfPayment = DateOfFine.Value.AddDays(30);
                            receivedSms.DateOfFine = DateOfFine;
                            receivedSms.LastDateOfPayment = LastDateOfPayment;
                        }
                        receivedSms.CreatedDate = DateTime.Now;
                        receivedSms.Article = fineData.Article;
                        receivedSms.Sent = false;
                        receivedSms.CarNumber = carnumber;
                        receivedSms.FinishStatus = SmsFinishStatus.Finished;
                        await _unitOfWorkRepository.ReceivedSmsRepository.AddAsync(receivedSms);
                        await _unitOfWorkRepository.SaveAsync();
                        break;
                    }
                    
                    receivedSms.FineStatus = fineData.Paid ? Domain.Enums.FineStatus.Paid : Domain.Enums.FineStatus.Unpaid;
                    await _unitOfWorkRepository.ReceivedSmsRepository.UpdateAsync(receivedSms);
                    await _unitOfWorkRepository.SaveAsync();
                    
                    break;
                }
                   
            }
        }
    }

    void TransilateToGeorgian(string georgianText,out string eglishText)
    {
        eglishText = "";
        Dictionary<string, string> georgianToLatinMap =new Dictionary<string, string>
        {
            { "ა", "a" }, { "ბ", "b" }, { "გ", "g" }, { "დ", "d" }, { "ე", "e" }, { "ვ", "v" }, { "ზ", "z" },
            { "თ", "t" }, { "ი", "i" }, { "კ", "k" }, { "ლ", "l" }, { "მ", "a" }, { "ნ", "n" }, { "ო", "o" },
            { "პ", "p" }, { "ჟ", "zh" }, { "რ", "r" }, { "ს", "s" }, { "ტ", "t" }, { "უ", "u" }, { "ფ", "f" }, 
            { "ქ", "q" }, { "ღ", "gh" }, { "ყ", "y" }, { "შ", "sh" }, { "ჩ", "ch" }, { "ც", "c" }, { "ძ", "dz" },
            { "წ", "w" }, { "ჭ", "tch" },
            { "ხ", "kh" }, { "ჯ", "j" }
        };
        foreach (char georgianLetter in georgianText)
        {
            if (georgianToLatinMap.TryGetValue(georgianLetter.ToString(), out string latinEquivalent))
            {
                eglishText += latinEquivalent;
            }
        }
        eglishText += new string(georgianText.Skip(2).ToArray());
    }
}

