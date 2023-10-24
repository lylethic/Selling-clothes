using ClothesWeb.Data;
using ClothesWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ClothesWeb.Areas.Customer.Controllers
{
  [Area("Customer")]
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
      IEnumerable<Product> products = _db.Product.Include("Category").ToList();
      return View(products);
    }

    public IActionResult Privacy()
    {
      return View();
    }
    public IActionResult Product_List()
    {
      return View();
    }
    public IActionResult Catagori()
    {
      IEnumerable<Product> products = _db.Product.Include("Category").ToList();
      return View(products);
    }

    //public IActionResult Product_Details(int productId)
    //{
    //  Product products = _db.Product.Include("Category").FirstOrDefault(sp => sp.Id_Product == productId);
    //  return View(products);
    //}

    [HttpGet]
    public IActionResult Product_Details(int productId)
    {
      Cart cart = new Cart()
      {
        LoaiId = productId,
        Product = _db.Product.Include("Category").FirstOrDefault(sp => sp.Id_Product == productId)!,
        Quantity = 1,
      };
      return View(cart);
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}