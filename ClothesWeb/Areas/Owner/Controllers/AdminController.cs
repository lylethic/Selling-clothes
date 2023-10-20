using ClothesWeb.Data;
using ClothesWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClothesWeb.Areas.Admin.Controllers
{
  [Area("Owner")]
  public class AdminController : Controller
  {
    private readonly ApplicationDbContext _db;
    public AdminController(ApplicationDbContext db)
    {
      _db = db;
    }

    public IActionResult Index()
    {
      return View();
    }

    public IActionResult Dashboard()
    {
      IEnumerable<Product> products = _db.Product.Include("Category").ToList();
      return View(products);
    }

    [HttpGet]
    public IActionResult Upsert(int id)
    {
      Product product = new Product();
      IEnumerable<SelectListItem> dstheloai = _db.Categories.Select(
        product => new SelectListItem
        {
          Value = product.LoaiId.ToString(),
          Text = product.Name,
        });

      ViewBag.Dstheloai = dstheloai;

      if (id == 0)
      {
        return View(product);
      }
      else
      {
        product = _db.Product.Include("Category").FirstOrDefault(product => product.Id_Product == id);
        return View(product);
      }
    }

    [HttpPost]
    public IActionResult Upsert(Product product)
    {
      if (ModelState.IsValid)
      {
        if (product.Id_Product == 0)
        {
          _db.Product.Add(product);
        }
        else
        {
          _db.Product.Update(product);
        }
        _db.SaveChanges();
        return RedirectToAction("Dashboard");
      }
      return View();
    }

    public IActionResult Delete(int id)
    {
      var product = _db.Product.FirstOrDefault(product => product.Id_Product == id);
      if (product == null)
      {
        return NotFound();
      }
      _db.Product.Remove(product);
      _db.SaveChanges();

      //return Json(new { success = true });
      return RedirectToAction("Dashboard");
    }

    public IActionResult Login()
    {
      return View();
    }

    public IActionResult Register()
    {
      return View();
    }

    public IActionResult Button()
    {
      return View();
    }

    public IActionResult Alerts()
    {
      return View();
    }

    public IActionResult Card()
    {
      return View();
    }

    public IActionResult Forms()
    {
      return View();
    }

    public IActionResult Typography()
    {
      return View();
    }

    public IActionResult Icons()
    {
      return View();
    }

    public IActionResult Sample_Page()
    {
      return View();
    }
  }
}
