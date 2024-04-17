using FineApi.Service.DTOs;

namespace FineApi.Service.Abstractions;

public interface ILoggerService
{
    public Task PublishLog(ErrorDto log);
}