using FineApi.Domain.Models;

namespace FineApi.Domain.Abstractions;

public interface IUserCarInformationRepository : IGenericRepository<UserCarInformation>
{
    public Task<IList<UserCarInformation>> GetAllCarsCanBePoliceCheck();
}


