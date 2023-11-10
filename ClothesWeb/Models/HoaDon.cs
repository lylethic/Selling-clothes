using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesWeb.Models
{
  public class HoaDon
  {
    [Key]
    public int Id { get; set; }
    public string ApplicationUserId { get; set; }
    [ForeignKey("ApplicationUserId")]
    [ValidateNever]
    public ApplicationUser User { get; set; }
    public double TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }

  }
}
