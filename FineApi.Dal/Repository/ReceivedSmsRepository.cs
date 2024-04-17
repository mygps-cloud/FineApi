using FineApi.Domain.Models;
using FineApi.Service.Abstractions;

namespace FineApi.Dal.Repository
{
    public class ReceivedSmsRepository : GenericRepository<ReceivedSms>, IReceivedSmsRepository
	{
		public ReceivedSmsRepository(FineDbContext dbcontext):base(dbcontext) { }
    }
}

