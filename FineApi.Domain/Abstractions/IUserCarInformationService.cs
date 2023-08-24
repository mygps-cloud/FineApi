
using FineApi.Domain.DTOs;

namespace FineApi.Domain.Abstractions
{
    public interface IUserCarInformationService
    {
        Task<IList<UserCarInformationDto>> GetAllUserCarInformation();
    }
}
