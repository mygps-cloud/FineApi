using System.ComponentModel.DataAnnotations;

namespace FineApi.Domain.Models;

public class Logs
{
    [Key]
    public int Id                    {get;}
    public string Errors             {get; set;}
    public DateTime ErrorTime        {get; set;}
    public int ?UserCarInformationId  {get; set;}
    public UserCarInformation? UserCarInformation { get; set; }
}