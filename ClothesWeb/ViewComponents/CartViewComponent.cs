using ClothesWeb.Data;
using ClothesWeb.Migrations;
using ClothesWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ClothesWeb.ViewComponents
{
  [Area("Customer")]
  [Authorize]
  public class CartViewComponent : ViewComponent
  {
    private readonly ApplicationDbContext _db;

    public CartViewComponent(ApplicationDbContext db)
    {
      _db = db;
    }

    public IViewComponentResult Invoke()
    {
      // is Login
      if (User.Identity.IsAuthenticated)
      {
        var identity = (ClaimsIdentity)User.Identity;
        var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

        CartViewModel cart = new CartViewModel()
        {
          listCart = _db.Carts
          .Include("Product")
          .Where(x => x.ApplicationUserId == claim.Value).ToList()
        };
        return View(cart);
      }
      else { return View(new CartViewModel()); }
    }
  }
}
