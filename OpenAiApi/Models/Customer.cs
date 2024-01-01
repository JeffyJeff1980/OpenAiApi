using System.ComponentModel.DataAnnotations.Schema;

namespace OpenAiApi.Models
{
  [Table("Customers")]
  public class Customer
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal CustomerId { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Gender { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Phone { get; set; }
  }
}