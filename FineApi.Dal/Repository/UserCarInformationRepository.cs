using FineApi.Domain.Abstractions;
using FineApi.Domain.Models;

namespace FineApi.Dal.Repository
{
	public class UserCarInformationRepository:GenericRepository<UserCarInformation>,IUserCarInformationRepository
	{
		public UserCarInformationRepository(FineDbContext dbcontext):base(dbcontext) {}
	}
}

