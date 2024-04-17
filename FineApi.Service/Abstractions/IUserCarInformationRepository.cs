using FineApi.Domain.Models;

namespace FineApi.Service.Abstractions;

public interface IUserCarInformationRepository : IGenericRepository<UserCarInformation>
{
    public Task<IList<UserCarInformation>> GetAllCarsCanBePoliceCheck();
}


