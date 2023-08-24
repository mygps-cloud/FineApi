namespace FineApi.Domain.Abstractions;
public interface IUnitOfWorkRepository:IDisposable
{
    IUserCarInformationRepository UserCarInformationRepository { get; }
    public IReceivedSmsRepository ReceivedSmsRepository { get; }
    Task<int> SaveAsync();
    public int Save();
}

