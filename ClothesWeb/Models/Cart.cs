using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClothesWeb.Models
{
  public class Cart
  {
    [Key]
    public int Id { get; set; }

    public int ProductId { get; set; }

    [ForeignKey("ProductId")]
    [ValidateNever]
    public Product Product { get; set; }

    public int Quantity { get; set; }
    public string? Size { get; set; }

    public bool isCheckout { get; set; }

    [NotMapped]
    public double ProductPrice { get; set; }

    public string ApplicationUserId { get; set; }

    [ForeignKey("ApplicationUserId")]
    [ValidateNever]
    public ApplicationUser ApplicationUser { get; set; }
  }
}
