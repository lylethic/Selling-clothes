using ClothesWeb.Data;
using ClothesWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothesWeb.ViewComponents
{
  public class CartViewComponent : ViewComponent
  {
    private readonly ApplicationDbContext _db;

    public CartViewComponent(ApplicationDbContext db)
    {
      _db = db;
    }

    public IViewComponentResult Invoke()
    {
      IEnumerable<Cart> carts = _db.Carts.ToList();
      return View(carts);
    }
  }
}
