using Fine.Api.Application.ServiceContracts;
using Fine.Api.DataAccess.Contracts.Entities;
using Fine.Api.DataAccess.Contracts.Repositories;
using Fine.Api.Exceptions;

namespace Fine.Api.Application.Services
{
    public class ReceivedSmsService : IReceivedSmsService
    {

        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        public ReceivedSmsService(IUnitOfWorkRepository unitOfWorkRepository) 
        {
            _unitOfWorkRepository = unitOfWorkRepository;
        }
        public async Task UpdateReceivedSms(string receiptNumber, bool paid)
        {
            var receivedSms = await _unitOfWorkRepository.ReceivedSmsRepository.FindAsync(x => x.ReceiptNumber == receiptNumber);
            if (receivedSms == null) throw new NoReceivedSmsOnThisReceiptNumberException();
            if (paid)
            {
                    receivedSms.FineStatus = Business.Enums.FineStatus.Paid;
                    await _unitOfWorkRepository.ReceivedSmsRepository.UpdateAsync(receivedSms);
                    await _unitOfWorkRepository.SaveAsync();
            }
            else
            {
                receivedSms.FineStatus=Business.Enums.FineStatus.Unpaid;
                await _unitOfWorkRepository.ReceivedSmsRepository.UpdateAsync(receivedSms);
                await _unitOfWorkRepository.SaveAsync();
            }
        }
    }
}
