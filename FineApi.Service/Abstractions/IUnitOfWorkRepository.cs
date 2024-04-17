namespace FineApi.Service.Abstractions;
public interface IUnitOfWorkRepository:IDisposable
{
    public IUserCarInformationRepository UserCarInformationRepository { get; }
    public IReceivedSmsRepository ReceivedSmsRepository { get; }
    public IEmailSenderRepository EmailSenderRepository { get; }
    public ISMSFromPoliceFideFineRepository SmsFromPoliceFideFineRepository { get; }
    public ILoggerRepository LoggerRepository { get; }
    Task<int> SaveAsync();
    public int Save();
}

