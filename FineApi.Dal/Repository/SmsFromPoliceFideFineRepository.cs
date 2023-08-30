using FineApi.Domain.Abstractions;
using FineApi.Domain.Models;

namespace FineApi.Dal.Repository;

public class SmsFromPoliceFideFineRepository : GenericRepository<SMSFromPoliceVideoFine>, ISMSFromPoliceFideFineRepository
{
    public SmsFromPoliceFideFineRepository(FineDbContext dbcontext):base(dbcontext) { }
}