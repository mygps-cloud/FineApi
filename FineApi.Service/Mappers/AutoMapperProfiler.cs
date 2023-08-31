using AutoMapper;
using FineApi.Domain.DTOs;
using FineApi.Domain.Models;

namespace FineApi.Service.Mappers;

public class AutoMapperProfiler : Profile
{
    public AutoMapperProfiler()
    {
        CreateMap<FineDataDto, ReceivedSms>().ReverseMap();
        CreateMap<UserCarInformation, UserCarInformationDto>().ReverseMap();
    }
}
