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

    public IViewComponentResult ViewCartItem(int Id)
    {
      // Retrieve the cart item based on the provided ID
      var cartItem = _db.Carts.FirstOrDefault(c => c.ProductId == Id);

      if (cartItem == null)
      {
        // Handle the case where the cart item is not found
        return View("CartItemNotFound");
      }

      // Process and display the cart item
      return View(cartItem);
    }
  }
}
