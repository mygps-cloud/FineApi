using System.ComponentModel.DataAnnotations;

namespace FineApi.Domain.Models;

public class CreatorsInfo
{
    [Key]
    public int Creator { get; set; }
    public  string? Company { get; set; }
    public  string? UserNumbers { get; set; }
    public  string? Email { get; set; }
    public bool?CanBeCheckPoliceFines { get; set; }
    public List<UserCarInformation>? UserCarInformation { get; set; }
}