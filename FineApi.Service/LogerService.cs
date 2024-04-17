using FineApi.Domain.Models;
using FineApi.Service.Abstractions;
using FineApi.Service.DTOs;

namespace FineApi.Service;

public class LoggerService:ILoggerService
{
    private IUnitOfWorkRepository _unitOfWork;

    public LoggerService(IUnitOfWorkRepository unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task PublishLog(ErrorDto log)
    {
        var result=await _unitOfWork.UserCarInformationRepository.
            FirstOrDefaultAsync(x => x.CarNumber == log.CarNumber || x.TechPassportId == log.TechId);
        
        var anyError=await _unitOfWork.LoggerRepository.
            AnyAsync(x => x.UserCarInformationId ==result.Id);
        
        if(anyError)
            return;
        
        var logs = new Logs()
        {
            Errors = log.Error,
            ErrorTime = DateTime.Now,
            UserCarInformationId = result.Id
        };
        await  _unitOfWork.LoggerRepository.AddAsync(logs);
        await _unitOfWork.SaveAsync();
    }
}