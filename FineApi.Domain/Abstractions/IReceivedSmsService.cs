using FineApi.Domain.DTOs;

namespace FineApi.Domain.Abstractions;
public interface IReceivedSmsService
{
    Task UpdateReceivedSms(List<FineDataDto> data);
}
