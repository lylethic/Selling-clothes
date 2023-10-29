using ClothesWeb.Data;
using ClothesWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;

namespace ClothesWeb.Areas.Customer.Controllers
{
  [Area("Customer")]
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
  }
}
