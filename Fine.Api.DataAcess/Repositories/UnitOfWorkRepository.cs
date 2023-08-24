using Fine.Api.DataAccess.Contracts.Repositories;
using Fine.Api.DataAcess.DbContexts;

namespace Fine.Api.DataAcess.Repositories
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private readonly FineDbContext _dbContext;
        public IUsercarInformationRepository UserCarInformationRepository { get; }
        public IReceivedSmsRepository ReceivedSmsRepository { get; }
        private bool _disposed;
        public UnitOfWorkRepository(FineDbContext dbContext,
            IUsercarInformationRepository userCarsRepository,IReceivedSmsRepository receivedSmsRepository)
        {
            _dbContext = dbContext;
            this.UserCarInformationRepository = userCarsRepository;
            this.ReceivedSmsRepository = receivedSmsRepository; 
            _disposed = false;
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed) 
            {
                if(disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
