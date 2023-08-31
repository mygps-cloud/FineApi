using AutoMapper;
using FineApi.Domain.Abstractions;
using FineApi.Domain.DTOs;
using FineApi.Domain.Models;
using FineApi.Service.Exception;

namespace FineApi.Service;
public class ReceivedSmsService : IReceivedSmsService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWorkRepository _unitOfWorkRepository;
    public ReceivedSmsService(IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper)
    {
        _unitOfWorkRepository = unitOfWorkRepository;
        _mapper = mapper;
    }
    public async Task UpdateReceivedSms(List<FineDataDto> data)
    {
        string latinText;
        string fineText="";
        var userCarInformation = await _unitOfWorkRepository.UserCarInformationRepository.GetAll()!;
        foreach (var fineData in data)
        {
            foreach (var carInformation in userCarInformation)
            {
                if (fineData.ReceiptNumber.Contains(carInformation.CarNumber)) 
                    fineText=fineData.ReceiptNumber.Substring(0,
                        fineData.ReceiptNumber.Length - carInformation.CarNumber.Length);
            }
            TransilateToGeorgian(fineText,out latinText);
            var receivedSms = await _unitOfWorkRepository.ReceivedSmsRepository.FirstOrDefaultAsync(x => x.ReceiptNumber == latinText);
            if (receivedSms == null)
            {
                receivedSms = new ReceivedSms();
                fineData.ReceiptNumber = latinText;
                receivedSms.FineStatus = fineData.Paid == true ? Domain.Enums.FineStatus.Paid : Domain.Enums.FineStatus.Unpaid;
                receivedSms.ReceiptNumber = fineData.ReceiptNumber;
                receivedSms.Amount = fineData.Amount;
                receivedSms.Deleted = true;
                receivedSms.Parsed = true;
                receivedSms.Sender = "POLICE";
                receivedSms.Term = 30;
                receivedSms.CreatedDate = DateTime.Parse(fineData.Date);
                receivedSms.Article = fineData.Article;
                receivedSms.Sent = true;
                await _unitOfWorkRepository.ReceivedSmsRepository.AddAsync(receivedSms);
                   
                await _unitOfWorkRepository.SaveAsync();
                continue;
            }
            
            receivedSms.FineStatus = fineData.Paid ? Domain.Enums.FineStatus.Paid : Domain.Enums.FineStatus.Unpaid;

            await _unitOfWorkRepository.ReceivedSmsRepository.UpdateAsync(receivedSms);
        }

       if(_unitOfWorkRepository.ReceivedSmsRepository.StateChanged())
            await _unitOfWorkRepository.SaveAsync();
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

