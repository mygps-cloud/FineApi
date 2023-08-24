using System;
using Microsoft.EntityFrameworkCore;
using Fine.Api.DataAccess.Contracts.Entities;
using Fine.Api.DataAccess.Contracts.Repositories;
using Fine.Api.DataAcess.DbContexts;

namespace Fine.Api.DataAcess.Repositories
{
    public class ReceivedSmsRepository : GenericRepository<ReceivedSms>, IReceivedSmsRepository
	{
		public ReceivedSmsRepository(FineDbContext dbcontext):base(dbcontext)
        { 
		}
    }
}

