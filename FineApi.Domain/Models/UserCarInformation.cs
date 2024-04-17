using System.ComponentModel.DataAnnotations;

namespace FineApi.Domain.Models;
public class UserCarInformation
{
    [Key]
    public int Id { get; set; }
    public  string CarNumber { get; set; }
    public  string? TechPassportId { get; set; }
    public  int? CompanyId { get; set; }
    public CreatorsInfo? CreatorsInfo { get; set; }
    public List<Logs>? Logs { get; set; }
}

