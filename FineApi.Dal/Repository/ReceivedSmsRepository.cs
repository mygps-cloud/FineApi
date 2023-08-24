using FineApi.Domain.Abstractions;
using FineApi.Domain.Models;

namespace FineApi.Dal.Repository
{
    public class ReceivedSmsRepository : GenericRepository<ReceivedSms>, IReceivedSmsRepository
	{
		public ReceivedSmsRepository(FineDbContext dbcontext):base(dbcontext) { }
    }
}

