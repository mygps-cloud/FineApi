
using FineApi.Domain.Abstractions;
using FineApi.Domain.DTOs;
using FineApi.Service.Exception;

namespace FineApi.Service;
public class ReceivedSmsService : IReceivedSmsService
{
    private readonly IUnitOfWorkRepository _unitOfWorkRepository;
    public ReceivedSmsService(IUnitOfWorkRepository unitOfWorkRepository) 
    {
        _unitOfWorkRepository = unitOfWorkRepository;
    }
    public async Task UpdateReceivedSms(List<FineDataDto> data)
    {
        foreach (var fineData in data)
        {
            var receivedSms = await _unitOfWorkRepository.ReceivedSmsRepository.SingleAsync(x => x.ReceiptNumber == fineData.ReceiptNumber);
            if (receivedSms == null) throw new NoReceivedSmsOnThisReceiptNumberException();
            
            receivedSms.FineStatus = fineData.Paid ? Domain.Enums.FineStatus.Paid : Domain.Enums.FineStatus.Unpaid;

            await _unitOfWorkRepository.ReceivedSmsRepository.UpdateAsync(receivedSms);
        }
        await _unitOfWorkRepository.SaveAsync();
    }
}

