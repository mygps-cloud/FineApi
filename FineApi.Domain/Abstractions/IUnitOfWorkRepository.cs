﻿namespace FineApi.Domain.Abstractions;
public interface IUnitOfWorkRepository:IDisposable
{
    public IUserCarInformationRepository UserCarInformationRepository { get; }
    public IReceivedSmsRepository ReceivedSmsRepository { get; }
    public IEmailSenderRepository EmailSenderRepository { get; }
    public ISMSFromPoliceFideFineRepository SmsFromPoliceFideFineRepository { get; }
    Task<int> SaveAsync();
    public int Save();
}

