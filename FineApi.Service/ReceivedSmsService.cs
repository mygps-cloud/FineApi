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
               var smsFromPoliceResult= await _unitOfWorkRepository.SmsFromPoliceFideFineRepository.FirstOrDefaultAsync(x => x.ReceiptNumber == latinText);
               if (smsFromPoliceResult is null)
               {
                   await _unitOfWorkRepository.SmsFromPoliceFideFineRepository.AddAsync(
                       _mapper.Map<SMSFromPoliceVideoFine>(fineData));
                   
                   await _unitOfWorkRepository.SaveAsync();
               }
               else
               {
                   smsFromPoliceResult.Paid = fineData.Paid ? true : false;
                   await _unitOfWorkRepository.SmsFromPoliceFideFineRepository.UpdateAsync(smsFromPoliceResult);
                   await _unitOfWorkRepository.SaveAsync();
               }
            }
            //სხვა ტეიბლში გატანა სმსების ჩაწერა თუ არ არსებობს თუ არსებობს განახლება.
                //განახლდება სტატუსი
            
            receivedSms.FineStatus = fineData.Paid ? Domain.Enums.FineStatus.Paid : Domain.Enums.FineStatus.Unpaid;

            await _unitOfWorkRepository.ReceivedSmsRepository.UpdateAsync(receivedSms);
        }

       if(_unitOfWorkRepository.ReceivedSmsRepository.StateChanged())
            await _unitOfWorkRepository.SaveAsync();
       else
       {
           throw new InvalidOperationException("Can't Process Data");
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

