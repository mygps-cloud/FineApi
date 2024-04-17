using FineApi.Domain.Models;
using FineApi.Service.Abstractions;

namespace FineApi.Dal.Repository;

public class EmailSenderRepository: GenericRepository<CreatorsInfo>, IEmailSenderRepository
{
    public EmailSenderRepository(FineDbContext context) : base(context)
    { }
}