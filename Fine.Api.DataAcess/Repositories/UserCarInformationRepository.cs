using System;
using Fine.Api.DataAccess.Contracts.Entities;
using Fine.Api.DataAccess.Contracts.Repositories;
using Fine.Api.DataAcess.DbContexts;

namespace Fine.Api.DataAcess.Repositories
{
	public class UserCarInformationRepository:GenericRepository<UserCarInformation>,IUsercarInformationRepository
	{
		public UserCarInformationRepository(FineDbContext dbcontext):base(dbcontext)
		{
		}
	}
}

