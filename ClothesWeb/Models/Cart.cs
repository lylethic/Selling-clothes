using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClothesWeb.Models
{
  public class Cart
  {
    [Key]
    public int IdCart { get; set; }
    public int LoaiId { get; set; }
    [ForeignKey("LoaiId")]
    [ValidateNever]
    public Product Product { get; set; }

    public int Quantity { get; set; }

    public string AppnUserId { get; set; }
    [ForeignKey("AppnUserId")]
    [ValidateNever]
    public ApplicationUser ApplicationUser { get; set; } 
  }
}
