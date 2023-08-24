﻿using FineApi.Domain.Abstractions;

namespace FineApi.Dal.Repository
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private readonly FineDbContext _dbContext;
        private IUserCarInformationRepository _userCarInformationRepository;
        private IReceivedSmsRepository _receivedSmsRepository;
        public UnitOfWorkRepository(FineDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IUserCarInformationRepository UserCarInformationRepository => _userCarInformationRepository ??= new UserCarInformationRepository(_dbContext);
        public IReceivedSmsRepository ReceivedSmsRepository => _receivedSmsRepository ??= new ReceivedSmsRepository(_dbContext);
        public Task<int> SaveAsync() => _dbContext.SaveChangesAsync();
        public int Save() => _dbContext.SaveChanges();
        public void Dispose()=> _dbContext.Dispose();
        
    }
}
 