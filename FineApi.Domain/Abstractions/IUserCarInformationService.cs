
using FineApi.Domain.DTOs;

namespace FineApi.Domain.Abstractions
{
    public interface IUserCarInformationService
    {
        ValueTask<UserCarInformationDto> GetAllUserCarInformation();
    }
}
