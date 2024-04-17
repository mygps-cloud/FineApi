using FineApi.Domain.Models;
using FineApi.Service.Abstractions;

namespace FineApi.Dal.Repository;

public class SmsFromPoliceFideFineRepository : GenericRepository<SMSFromPoliceVideoFine>, ISMSFromPoliceFideFineRepository
{
    public SmsFromPoliceFideFineRepository(FineDbContext dbcontext):base(dbcontext) { }
}