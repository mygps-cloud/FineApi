
using FineApi.Service.DTOs;

namespace FineApi.Service.Abstractions
{
    public interface IUserCarInformationService
    {
        ValueTask<UserCarInformationDto> GetAllUserCarInformation(NextCarDTO next);
    }
}
