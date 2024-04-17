using FineApi.Service.DTOs;

namespace FineApi.Service.Abstractions;
public interface IReceivedSmsService
{
    Task UpdateReceivedSms(List<FineDataDto> data);
}
