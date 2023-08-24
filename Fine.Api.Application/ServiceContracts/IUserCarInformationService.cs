using Fine.Api.Business.DTO_s;

namespace Fine.Api.Application.ServiceContracts
{
    public interface IUserCarInformationService
    {
        Task<IList<UserCarInformationDTO>> GetAllUserCarInformation();
    }
}
