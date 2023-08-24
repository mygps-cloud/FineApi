
using FineApi.Domain.Abstractions;
using FineApi.Service.Exception;

namespace FineApi.Service;
public class ReceivedSmsService : IReceivedSmsService
{
    private readonly IUnitOfWorkRepository _unitOfWorkRepository;
    public ReceivedSmsService(IUnitOfWorkRepository unitOfWorkRepository) 
    {
        _unitOfWorkRepository = unitOfWorkRepository;
    }
    public async Task UpdateReceivedSms(string receiptNumber, bool paid)
    {
        var receivedSms = await _unitOfWorkRepository.ReceivedSmsRepository.SingleAsync(x => x.ReceiptNumber == receiptNumber);
        if (receivedSms == null) throw new NoReceivedSmsOnThisReceiptNumberException();
        
        receivedSms.FineStatus = paid ? Domain.Enums.FineStatus.Paid : Domain.Enums.FineStatus.Unpaid;

        await _unitOfWorkRepository.ReceivedSmsRepository.UpdateAsync(receivedSms);
        await _unitOfWorkRepository.SaveAsync();
    }
}

