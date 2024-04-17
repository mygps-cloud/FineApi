using FineApi.Domain.Models;
using FineApi.Service.Abstractions;
using FineApi.Service.DTOs;

namespace FineApi.Dal.Repository;

public class LoggerRepository:GenericRepository<Logs>,ILoggerRepository
{
    public LoggerRepository(FineDbContext dbcontext):base(dbcontext) {}
}