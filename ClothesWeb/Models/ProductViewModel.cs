using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClothesWeb.Models
{
  public class ProductViewModel
  {
    public IQueryable<Product> Products { get; set; }
    public int IdProduct { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? Size { get; set; }
    public string? Color { get; set; }
    public int LoaiId { get; set; }
    public Category Category { get; set; }
  }
}
