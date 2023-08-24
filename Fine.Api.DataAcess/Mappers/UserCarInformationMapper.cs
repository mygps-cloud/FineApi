using Fine.Api.Business.DTO_s;
using Fine.Api.DataAccess.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fine.Api.DataAcess.Mappers
{
    public static class UserCarInformationMapper
    {
        public static UserCarInformationDTO MapToDTO(UserCarInformation entity)
        {
            return new UserCarInformationDTO()
            {
                Id = entity.Id,
                CarNumber = entity.CarNumber,
                TechPassportId = entity.TechPassportId,
            };
        }
    }
}
