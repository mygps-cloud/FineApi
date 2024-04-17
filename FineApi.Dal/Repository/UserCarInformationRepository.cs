using FineApi.Domain.Abstractions;
using FineApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FineApi.Dal.Repository
{
	public class UserCarInformationRepository:GenericRepository<UserCarInformation>,IUserCarInformationRepository
	{
		public UserCarInformationRepository(FineDbContext dbcontext):base(dbcontext) {}

		public async Task<IList<UserCarInformation>> GetAllCarsCanBePoliceCheck()
		{
			return Task.FromResult(this.Set.Select(x=>x).Where(x=>x.CreatorsInfo.CanBeCheckPoliceFines == true && x.TechPassportId != null)).Result.ToList();
		}
	}
}

