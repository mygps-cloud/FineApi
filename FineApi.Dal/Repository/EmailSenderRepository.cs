using FineApi.Domain.Abstractions;
using FineApi.Domain.Models;

namespace FineApi.Dal.Repository;

public class EmailSenderRepository: GenericRepository<Company>, IEmailSenderRepository
{
    public EmailSenderRepository(FineDbContext context) : base(context)
    { }
}