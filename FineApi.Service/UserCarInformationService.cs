using FineApi.Domain.Abstractions;
using FineApi.Domain.DTOs;
using FineApi.Service.Exception;
using FineApi.Service.Mappers;

namespace FineApi.Service;
public class UserCarInformationService : IUserCarInformationService
{
    private readonly IUnitOfWorkRepository _unitOfWorkRepository;
    public UserCarInformationService(IUnitOfWorkRepository unitOfWorkRepository)
    {
        _unitOfWorkRepository = unitOfWorkRepository;
    }

    public async Task<IList<UserCarInformationDto>> GetAllUserCarInformation()
    {
        List<UserCarInformationDto> userCars = new();
        var userCarInformation = await _unitOfWorkRepository.UserCarInformationRepository.GetAll()!;
        
        foreach(var item in userCarInformation.Where(x=>x.CarNumber!=null))
        {
            bool exists = await _unitOfWorkRepository.ReceivedSmsRepository.AnyAsync(x=>x.CarNumber==item.CarNumber);
            
            if (exists)
                userCars.Add(UserCarInformationMapper.MapToDTO(item));
        }
        
        if (!userCars.Any()) throw new NoUserCarInformationException();
        return userCars;
    }
}
