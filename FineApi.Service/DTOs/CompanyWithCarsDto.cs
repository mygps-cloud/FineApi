namespace FineApi.Service.DTOs;

public class CompanyWithCarsDto
{
    public string Email { get; set; }

    List<UserCarInformationDto> UserCarInformation { get; set; }
}