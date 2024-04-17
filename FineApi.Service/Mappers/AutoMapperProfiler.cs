using AutoMapper;
using FineApi.Domain.Models;
using FineApi.Service.DTOs;

namespace FineApi.Service.Mappers;

public class AutoMapperProfiler : Profile
{
    public AutoMapperProfiler()
    {
        CreateMap<FineDataDto, ReceivedSms>().ReverseMap();
        CreateMap<UserCarInformation, UserCarInformationDto>().ReverseMap();
    }
}
