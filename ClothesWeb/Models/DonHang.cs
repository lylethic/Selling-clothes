using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesWeb.Models
{
  public class DonHang
  {
    [Key]
    public int Id { get; set; }

    public int OrderDetailId { get; set; }
    [ForeignKey("OrderDetailId")]
    [ValidateNever]
    public ChiTietHoaDon ChiTietHoaDon { get; set; }

    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    [ValidateNever]
    public Product Product { get; set; }

    public int HoaDonId { get; set; }
    [ForeignKey("HoaDonId")]
    [ValidateNever]
    public HoaDon HoaDon { get; set; }

    public string ImgUrl { get; set; }
    public string NameProduct { get; set; }
    public double PriceOfProduct { get; set; }
    public double Total { get; set; }
    public int Quantity { get; set; }
    public DateTime? OrderDate { get; set; }
    public string? OrderStatus { get; set; }
    public bool IsCheckout { get; set; }
  }
}
