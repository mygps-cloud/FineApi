using FineApi.Domain.Abstractions;
using FineApi.Domain.Models;

namespace FineApi.Dal.Repository;

public class EmailSenderRepository: GenericRepository<CreatorsInfo>, IEmailSenderRepository
{
    public EmailSenderRepository(FineDbContext context) : base(context)
    { }
}