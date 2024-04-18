using System.ComponentModel.DataAnnotations;

namespace ClothesWeb.Models
{
  public class NhaCungCap
  {
    [Key]
    public string IdNhaCungCap { get; set; }

    public string TenCongTy { get; set; } = null!;

    public string Logo { get; set; } = null!;

    public string? NguoiLienLac { get; set; }

    public string Email { get; set; } = null!;

    public string? DienThoai { get; set; }

    public string? DiaChi { get; set; }

    public string? MoTa { get; set; }

    public virtual ICollection<Product> HangHoas { get; set; } = new List<Product>();
  }
}
