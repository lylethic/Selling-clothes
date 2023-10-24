using ClothesWeb.Data;
using ClothesWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClothesWeb.ViewComponents
{
  public class CategoryViewComponent : ViewComponent
  {
    private readonly ApplicationDbContext _db;

    public CategoryViewComponent(ApplicationDbContext db)
    {
      _db = db;
    }

    //
    public IViewComponentResult Invoke()
    {
      // Get products category
      IEnumerable<Category> cat = _db.Categories.ToList();
      return View(cat);
    }
  }
}
