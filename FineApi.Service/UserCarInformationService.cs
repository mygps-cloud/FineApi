using FineApi.Domain.Abstractions;
using FineApi.Domain.DTOs;
using FineApi.Service.Exception;
using FineApi.Service.Mappers;

namespace FineApi.Service;
public class UserCarInformationService : IUserCarInformationService
{
    static List<UserCarInformationDto> userCars = new();
    static int incrimentDta=0;
    private readonly IUnitOfWorkRepository _unitOfWorkRepository;
    
    public UserCarInformationService(IUnitOfWorkRepository unitOfWorkRepository)
    {
        _unitOfWorkRepository = unitOfWorkRepository;
    }
    public async ValueTask<UserCarInformationDto> GetAllUserCarInformation()
    {
        if (!userCars.Any())
        {
            var userCarInformation = await _unitOfWorkRepository.UserCarInformationRepository.GetAll()!;
        
            foreach(var item in userCarInformation)
            {
                bool exists = await _unitOfWorkRepository.ReceivedSmsRepository.AnyAsync(x=>x.CarNumber==item.CarNumber);
            
                if (exists)
                    userCars.Add(UserCarInformationMapper.MapToDTO(item));
            }
        
            if (!userCars.Any()) throw new NoUserCarInformationException();
        }
        incrimentDta++;
        if (incrimentDta > userCars.Count)
            throw new InvalidOperationException("Can't Process Data");
            
            return userCars[incrimentDta-1];
    }
}
