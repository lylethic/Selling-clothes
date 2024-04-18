using ClothesWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClothesWeb.Data
{
  public class ApplicationDbContext : IdentityDbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<ApplicationUser> ApplicationUser { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<HoaDon> HoaDons { get; set; }
    public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    public DbSet<DonHang> DonHangs { get; set; }
    public DbSet<NhaCungCap> NhaCungCaps { get; set; }
    public DbSet<NhapKho> NhapKhos { get; set; }
    public DbSet<PhieuNhapKho> PhieuNhapKhos { get; set; }
    public DbSet<ChiTietPhieuNhapKho> ChiTietPhieuNhapKhos { get; set; }
    public DbSet<TonKho> TonKhos { get; set; }
  }
}
