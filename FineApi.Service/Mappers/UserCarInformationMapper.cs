using FineApi.Domain.DTOs;
using FineApi.Domain.Models;

namespace FineApi.Service.Mappers;
public static class UserCarInformationMapper
{
    public static UserCarInformationDto MapToDTO(UserCarInformation entity)
    {
        return new UserCarInformationDto(entity.Id, entity.CarNumber, entity.TechPassportId);
    }
}

