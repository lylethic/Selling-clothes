using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClothesWeb.Models
{
  public class Product
  {
    [Key]
    public int Id_Product { get; set; }

    [Required]
    public string Name { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public double Price { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    [Required]
    public int LoaiId { get; set; }
    [ForeignKey("LoaiId")]
    [ValidateNever]
    public Category Category { get; set; }
  }
}
