using AutoMapper;
using FineApi.Domain.Abstractions;
using FineApi.Domain.DTOs;
using FineApi.Service.Exception;
using FineApi.Service.Mappers;

namespace FineApi.Service;
public class UserCarInformationService : IUserCarInformationService
{
    static List<UserCarInformationDto> userCars = new();
    static int incrimentDta=0;
    private readonly IMapper _mapper;
    private readonly IUnitOfWorkRepository _unitOfWorkRepository;
    
    public UserCarInformationService(IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper)
    {
        _unitOfWorkRepository = unitOfWorkRepository;
        _mapper = mapper;
    }
    public async ValueTask<UserCarInformationDto> GetAllUserCarInformation(NextCarDTO next)
    {
        if (!userCars.Any())
        {
            var userCarInformation = await _unitOfWorkRepository.UserCarInformationRepository.GetAll()!;
        
            foreach(var item in userCarInformation)
            {
                userCars.Add(_mapper.Map<UserCarInformationDto>(item));
            }
        
            if (!userCars.Any()) throw new NoUserCarInformationException();
        }
        if (!next.Gotonext)
        {
            if(incrimentDta<1)
                throw new InvalidOperationException("Can't Process Data,Choose First Car");
            
            return userCars[incrimentDta-1];
        }
        incrimentDta++;
        if (incrimentDta > userCars.Count)
        {
            incrimentDta = 0;
            throw new InvalidOperationException("Can't Process Data");
        }
        return userCars[incrimentDta - 1];
    }
}
