
using AutoMapper;
using Fine.Api.VMs;
using FineApi.Service.DTOs;

namespace Fine.Api;

public class AutoMapperProfiler : Profile
{
    public AutoMapperProfiler()
    {
        CreateMap<FineDataVm, FineDataDto>().ReverseMap();
        CreateMap<NexCarVM, NextCarDTO>();
        CreateMap<ErrorVM, ErrorDto>();
    }
}
