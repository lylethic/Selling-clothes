using ClothesWeb.Data;
using ClothesWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;

namespace ClothesWeb.Areas.Customer.Controllers
{
  [Area("Customer")]
  [Authorize]
  public class CartController : Controller
  {
    private readonly ApplicationDbContext _db;

    public CartController(ApplicationDbContext db)
    {
      _db = db;
    }

    public IActionResult Index()
    {
      //Get Infor of Account
      var identity = (ClaimsIdentity)User.Identity;
      var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

      //Get List of Product in Cart of User
      CartViewModel cart = new CartViewModel()
      {
        listCart = _db.Carts
        .Include("Product")
        .Where(x => x.ApplicationUserId == claim.Value).ToList()
      };

      foreach (var item in cart.listCart)
      {
        //Pay by product quantity.
        item.ProductPrice = item.Quantity * item.Product.Price;
        //Check out the shopping cart.
        cart.TotalPrice += item.ProductPrice;
      }
      return View(cart);
    }

    public IActionResult DeleteProduct(int cartId)
    {
      var cartItem = _db.Carts.FirstOrDefault(x => x.Id == cartId);

      _db.Carts.Remove(cartItem);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public IActionResult Giam(int cartId)
    {
      var cart = _db.Carts.FirstOrDefault(cart => cart.Id == cartId);
      cart.Quantity -= 1;
      if (cart.Quantity == 0)
      {
        _db.Carts.Remove(cart);
      }
      _db.SaveChanges();

      return RedirectToAction("Index");
    }

    public IActionResult Tang(int cartId)
    {
      var cart = _db.Carts.FirstOrDefault(cart => cart.Id == cartId);
      cart.Quantity += 1;
      _db.SaveChanges();

      return RedirectToAction("Index");
    }
  }
}
