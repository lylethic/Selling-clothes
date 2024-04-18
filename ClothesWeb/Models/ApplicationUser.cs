using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

namespace ClothesWeb.Models
{
  public class ApplicationUser : IdentityUser
  {
    [Required]
    public string Name { get; set; }

    public string? Address { get; set; }

    public string Password { get; set; }
  }
}
