using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FineApi.Domain.Models;

public class SMSFromPoliceVideoFine
{
    [Key]
    public int Id { get; set; }
    public string ReceiptNumber { get; set; }
    public string Date { get; set; }
    public string Article { get; set; }
    public decimal Amount { get; set; }
    [Column(TypeName = "bit")]
    public byte Paid { get; set; }
}