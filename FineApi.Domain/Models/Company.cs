namespace FineApi.Domain.Models;

public class Company
{
    public int Id { get; set; }
    public string TechPassportId { get; set; }
    public string? Email { get; set; }
    public List<UserCarInformation>? UserCarInformation { get; set; }
}