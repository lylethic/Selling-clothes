using Azure;
using ClothesWeb.Data;
using ClothesWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Security.Principal;
using X.PagedList;
using X.PagedList.Mvc.Core;

namespace ClothesWeb.Areas.Customer.Controllers
{
  [Area("Customer")]
  //[Authorize(Roles = "User")]
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _db;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
    {
      _logger = logger;
      _db = db;
    }

    public IActionResult Index()
    {
      IEnumerable<Product> products = _db.Products.Include("Category").ToList();
      return View(products);
    }

    public IActionResult Privacy()
    {
      return View();
    }

    //[HttpGet]
    //public IActionResult Product_List(int page = 1)
    //{
    //  IEnumerable<Product> products = _db.Products.Include("Category").ToList();
    //  const int pageSize = 12;
    //  page = page < 1 ? 1 : page;
    //  // Tổng số sản phẩm.
    //  int recsCount = products.Count();
    //  // Một đối tượng Pager được tạo để quản lý thông tin về phân trang, bao gồm số trang, trang hiện tại, và số lượng sản phẩm trên mỗi trang.
    //  var pager = new Pager(recsCount, page, pageSize);
    //  int recSkip = (page - 1) * pageSize;
    //  var data = products.Skip(recSkip).Take(pager.PageSize).ToList();
    //  this.ViewBag.Pager = pager;
    //  return View(data);
    //}

    //[HttpGet]
    //public IActionResult Product_List(int loaiId)
    //{
    //  IEnumerable<Product> product = _db.Products.Include("Category").Where(x => x.LoaiId == loaiId);
    //  return View(product);
    //}

    [HttpGet]
    public IActionResult Product_List(int loaiId, int page = 1)
    {
      const int pageSize = 8;
      IEnumerable<Product> products;

      if (loaiId != 0)
      {
        products = _db.Products.Include("Category")
                               .Where(x => x.LoaiId == loaiId)
                               .ToList();
      }
      else
      {
        products = _db.Products.Include("Category")
                               .ToList();
      }
      page = page < 1 ? 1 : page;
      int recsCount = products.Count();
      var pager = new Pager(recsCount, page, pageSize);
      int recSkip = (page - 1) * pageSize;
      var data = products.Skip(recSkip).Take(pager.PageSize).ToList();
      this.ViewBag.Pager = pager;
      this.ViewBag.LoaiId = loaiId;
      return View(data);
    }


    [HttpGet]
    public IActionResult Catagori(int loaiId, int page = 1)
    {
      IEnumerable<Product> products;

      if (loaiId != 0)
      {
        products = _db.Products.Include("Category")
                               .Where(x => x.LoaiId == loaiId)
                               .ToList();
      }
      else
      {
        products = _db.Products.Include("Category")
                               .ToList();
      }
      const int pageSize = 12;
      page = page < 1 ? 1 : page;
      int recsCount = products.Count();
      var pager = new Pager(recsCount, page, pageSize);
      int recSkip = (page - 1) * pageSize;
      var data = products.Skip(recSkip).Take(pager.PageSize).ToList();
      this.ViewBag.Pager = pager;
      this.ViewBag.LoaiId = loaiId;
      return View(data);
    }

    [HttpGet]
    public async Task<IActionResult> Sort(string sortOrder, string searchString)
    {
      ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
      ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
      ViewData["CurrentFilter"] = searchString;

      var products = from s in _db.Products select s;
      if (!String.IsNullOrEmpty(searchString))
      {
        products = products.Where(s => s.Name.Contains(searchString));
      }

      switch (sortOrder)
      {
        case "name_desc":
          products = products.OrderByDescending(s => s.Name);
          break;
        case "Price":
          products = products.OrderBy(s => s.Price);
          break;
        case "price_desc":
          products = products.OrderByDescending(s => s.Price);
          break;
        default:
          products = products.OrderBy(s => s.Name);
          break;
      }
      return View(await products.AsNoTracking().ToListAsync());
    }

    [HttpGet]
    public IActionResult Product_Details(int productId)
    {
      Cart cart = new Cart()
      {
        ProductId = productId,
        Product = _db.Products.Include("Category").FirstOrDefault(sp => sp.IdProduct == productId),
        Quantity = 1,
      };
      return View(cart);
    }

    [HttpPost]
    [Authorize]
    public IActionResult Product_Details(Cart cart)
    {
      var identity = (ClaimsIdentity)User.Identity;
      var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
      cart.ApplicationUserId = claim.Value;

      // Check product
      var cartdb = _db.Carts
        .FirstOrDefault(x => x.ProductId == cart.ProductId && x.Size == cart.Size && x.ApplicationUserId == cart.ApplicationUserId);
      var productId = _db.Products.Include("Category").FirstOrDefault(sp => sp.IdProduct == cart.ProductId);

      // ko tim thay => them moi
      if (cartdb == null)
      {
        var cartItem = new Cart()
        {
          ApplicationUserId = cart.ApplicationUserId,
          ProductId = cart.ProductId,
          Product = productId,
          Quantity = cart.Quantity,
          Size = cart.Size,
          isCheckout = true
        };

        _db.Carts.Add(cartItem);
      }
      // tim thay => tang quantity
      else
      {
        cartdb.Quantity += cart.Quantity;
      }
      _db.SaveChanges();

      return RedirectToAction("Catagori");
    }

    public IActionResult Blog()
    {
      return View();
    }
    public IActionResult Blog_Details()
    {
      return View();
    }
    public IActionResult Login()
    {
      return View();
    }
    public IActionResult Sign_Up()
    {
      return View();
    }
    public IActionResult Cart()
    {
      return View();
    }
    public IActionResult About()
    {
      return View();
    }
    public IActionResult Confirmation()
    {
      return View();
    }
    public IActionResult Checkout()
    {
      return View();
    }
    public IActionResult Contact()
    {
      return View();
    }

    [HttpGet]
    public IActionResult TimKiemSanPham(string name = "", int page = 1)
    {
      var products = from b in _db.Products select b;

      if (!string.IsNullOrEmpty(name))
      {
        products = products.Where(x => x.Name.Contains(name));
      }

      const int pageSize = 12;
      page = page < 1 ? 1 : page;
      int recsCount = products.Count();
      var pager = new Pager(recsCount, page, pageSize);
      int recSkip = (page - 1) * pageSize;
      var data = products.Skip(recSkip).Take(pager.PageSize).ToList();
      ViewBag.SearchString = name;
      this.ViewBag.Pager = pager;
      return View(data);
    }

    //[HttpGet]
    //public IActionResult TimKiemSanPham(string name)
    //{
    //  var products = from b in _db.Products select b;

    //  if (!string.IsNullOrEmpty(name))
    //  {
    //    products = products.Where(x => x.Name.Contains(name));
    //  }
    //  return View(products);
    //}

    public IActionResult DonHang(int hoadonId)
    {
      //Get Info of Account
      var identity = (ClaimsIdentity)User.Identity;
      var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

      IEnumerable<HoaDon> donhang = _db.HoaDons
        .Include(x => x.User)
        .Where(x => x.ApplicationUserId == claim.Value)
        .ToList();

      var userName = _db.Users.FirstOrDefault(x => x.Id == claim.Value).UserName;
      ViewBag.Name = userName;
      return View(donhang);
    }

    public IActionResult DonhangDetails(int id)
    {
      var chiTietHoaDonList = _db.ChiTietHoaDons
                                  .Include("HoaDon")
                                  .Where(chiTiet => chiTiet.HoaDonId == id)
                                  .ToList();

      var totalprice = _db.HoaDons.Where(x => x.Id == id).FirstOrDefault();
      if (totalprice != null)
      {
        ViewBag.Total = totalprice.TotalPrice;

        if (chiTietHoaDonList.Any())
        {
          return View(chiTietHoaDonList);
        }
      }

      return NotFound();
    }

    [HttpGet]
    public IActionResult GetUserName()
    {
      return View();
    }

    [HttpGet]
    [Authorize] // Restrict access to authenticated users
    public IActionResult TaiKhoan()
    {
      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the current user's ID
      var currentUser = _db.ApplicationUser.FirstOrDefault(u => u.Id == userId);

      if (currentUser != null)
      {
        return View(currentUser);
      }

      return NotFound();
    }

    [HttpPost]
    public IActionResult TaiKhoan(ApplicationUser updatedUser)
    {
      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      var currentUser = _db.ApplicationUser.FirstOrDefault(u => u.Id == userId);

      if (currentUser != null)
      {
        currentUser.Name = updatedUser.Name;
        currentUser.Email = updatedUser.Email;
        currentUser.Address = updatedUser.Address;
        currentUser.PhoneNumber = updatedUser.PhoneNumber;

        _db.SaveChanges(); // Save changes to the database

        return RedirectToAction("TaiKhoan"); // Redirect to the profile page
      }

      return NotFound(); // Handle if the user is not found
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}