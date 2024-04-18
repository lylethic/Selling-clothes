using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesWeb.Models
{
  public class NhapKho
  {
    [Key]
    public int Id_NhapKho { get; set; }

    public DateTime NgayNhapKho { get; set; }

    public string IdNCC { get; set; }
    [ForeignKey("IdNCC")]
    [ValidateNever]
    public NhaCungCap NhaCungCap { get; set; }

    public double TongTienHang { get; set; }

    public int Id_LoaiHangHoa { get; set; }
    [ForeignKey("Id_LoaiHangHoa")]
    [ValidateNever]
    public Category Category { get; set; }
  }

  public class PhieuNhapKho
  {
    [Key]
    public int Id_PhieuNhapKho { get; set; }

    public DateTime ThoiGianNhapKho { get; set; }

    public string Id_NhaCungCap { get; set; }
    [ForeignKey("Id_NhaCungCap")]
    [ValidateNever]
    public NhaCungCap NhaCungCap { get; set; }

    public double TongTienPhieuNhapKho { get; set; }
  }

  public class ChiTietPhieuNhapKho
  {
    [Key]
    public int Id_CTPNKho { get; set; }

    public int Id_PhieuNhapKho { get; set; }
    [ForeignKey("Id_PhieuNhapKho")]
    [ValidateNever]
    public PhieuNhapKho PhieuNhapKho { get; set; }

    public int Id_Product { get; set; }
    [ForeignKey("Id_Product")]
    [ValidateNever]
    public Product Product { get; set; }

    public int SoLuongNhapKho { get; set; }
    public double GiaNhapSanPham { get; set; }
  }

  public class TonKho
  {
    [Key]
    public int MaTonKho { get; set; }

    public int Id_PhieuNhapKho { get; set; }
    [ForeignKey("Id_PhieuNhapKho")]
    [ValidateNever]
    public PhieuNhapKho PhieuNhapKho { get; set; }

    public int Id_Product { get; set; }
    [ForeignKey("Id_Product")]
    [ValidateNever]
    public Product Product { get; set; }

    public string TenSanPham { get; set; }

    public int SoLuongTK { get; set; }

    public float? MucTonKhoToiThieu { get; set; } = 0;

    public double GiaBan { get; set; }
  }
}
