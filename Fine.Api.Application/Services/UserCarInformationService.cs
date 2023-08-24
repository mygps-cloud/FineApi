using Fine.Api.Application.ServiceContracts;
using Fine.Api.Business.DTO_s;
using Fine.Api.DataAccess.Contracts.Entities;
using Fine.Api.DataAccess.Contracts.Repositories;
using Fine.Api.DataAcess.Mappers;
using Fine.Api.Exceptions.UserCarExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fine.Api.Application.Services
{
    public class UserCarInformationService : IUserCarInformationService
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        public UserCarInformationService(IUnitOfWorkRepository unitOfWorkRepository)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
        }

        public async Task<IList<UserCarInformationDTO>> GetAllUserCarInformation()
        {
            List<UserCarInformationDTO> userCars = new();
            var userCarInformation = await _unitOfWorkRepository.UserCarInformationRepository.GetAllAsync()!;
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
}
