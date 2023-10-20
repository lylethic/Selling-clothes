using ClothesWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClothesWeb.Data
{
  public class ApplicationDbContext : IdentityDbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<ApplicationUser> ApplicationUser { get; set; }
  }
}
