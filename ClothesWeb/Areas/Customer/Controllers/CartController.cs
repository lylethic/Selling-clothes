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
        .Where(x => x.ApplicationUserId == claim.Value).ToList(),
        HoaDon = new HoaDon()
      };

      foreach (var item in cart.listCart)
      {
        //Pay by product quantity.
        item.ProductPrice = item.Quantity * item.Product.Price;
        //Check out the shopping cart.
        cart.HoaDon.TotalPrice += item.ProductPrice;
      }

      if (cart.listCart.Count() == 0)
      {
        return RedirectToAction("NoProductPage");
      }

      return View(cart);
    }

    [HttpGet]
    public IActionResult ThanhToan()
    {
      //Get Infor of Account
      var identity = (ClaimsIdentity)User.Identity;
      var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

      //Get List of Product in Cart of User
      CartViewModel cart = new CartViewModel()
      {
        listCart = _db.Carts
        .Include("Product")
        .Where(x => x.ApplicationUserId == claim.Value).ToList(),
        HoaDon = new HoaDon()
      };

      foreach (var item in cart.listCart)
      {
        //Pay by product quantity.
        item.ProductPrice = item.Quantity * item.Product.Price;
        //Check out the shopping cart.
        cart.HoaDon.TotalPrice += item.ProductPrice;
      }

      cart.HoaDon.User = _db.ApplicationUser.FirstOrDefault(user => user.Id == claim.Value);
      cart.HoaDon.Name = cart.HoaDon.User.Name;
      cart.HoaDon.Address = cart.HoaDon.User.Address;
      cart.HoaDon.PhoneNumber = cart.HoaDon.User.PhoneNumber;
      return View(cart);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ThanhToan(CartViewModel cart)
    {
      //Get Infor of Account
      var identity = (ClaimsIdentity)User.Identity;
      var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

      // Update cart and hoadon
      cart.listCart = _db.Carts
        .Include("Product")
        .Where(cart => cart.ApplicationUserId == claim.Value)
        .ToList();

      if (cart.listCart.Count() == 0)
      {
        TempData["EmptyCartMessage"] = "Chưa có sản phẩm trong giỏ hàng.";
        return RedirectToAction("Index", "Home");
      }
      cart.HoaDon.ApplicationUserId = claim.Value;
      cart.HoaDon.OrderDate = DateTime.Now;
      cart.HoaDon.OrderStatus = "Dang xac nhan";

      foreach (var item in cart.listCart)
      {
        //Pay by product quantity.
        item.ProductPrice = item.Quantity * item.Product.Price;
        //Check out the shopping cart.
        cart.HoaDon.TotalPrice += item.ProductPrice;
      }
      _db.HoaDons.Add(cart.HoaDon);
      _db.SaveChanges();

      //
      foreach (var item in cart.listCart)
      {
        ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon()
        {
          ProductId = item.ProductId,
          HoaDonId = cart.HoaDon.Id,
          Username = cart.HoaDon.Name,
          ProductPrice = item.ProductPrice,
          Quantity = item.Quantity,
        };
        _db.ChiTietHoaDons.Add(chiTietHoaDon);
        _db.SaveChanges();
      }
      //
      _db.Carts.RemoveRange(cart.listCart);
      _db.SaveChanges();

      //
      //foreach (var item in cart.DsChiTietHoaDon)
      //{
      //  DonHang donHang = new DonHang()
      //  {
      //    OrderDetailId = item.Id,
      //    ProductId = item.ProductId,
      //    HoaDonId = cart.HoaDon.Id,
      //    ImgUrl = item.Product.ImageUrl,
      //    NameProduct = item.Product.Name,
      //    PriceOfProduct = item.ProductPrice,
      //    Quantity = item.Quantity,
      //    OrderDate = item.HoaDon.OrderDate,
      //    OrderStatus = item.HoaDon.OrderStatus,
      //    Total = item.HoaDon.TotalPrice,
      //  };
      //  _db.DonHangs.Add(donHang);
      //  _db.SaveChanges();
      //}

      return RedirectToAction("Index", "Home");
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

    public IActionResult NoProductPage()
    {
      return View();
    }
  }
}
