using ClothesWeb.Data;
using ClothesWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClothesWeb.ViewComponents
{
  public class AdminPhanloaiViewComponent : ViewComponent
  {
    private readonly ApplicationDbContext _db;

    public AdminPhanloaiViewComponent(ApplicationDbContext db)
    {
      _db = db;
    }

    public IViewComponentResult Invoke()
    {
      //Get products category
      IEnumerable<Category> cat = _db.Categories.ToList();
      return View(cat);
    }
  }
}
