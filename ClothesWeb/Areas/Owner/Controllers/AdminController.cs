using Azure;
using ClothesWeb.Data;
using ClothesWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using System.Security.Claims;
using X.PagedList;

namespace ClothesWeb.Areas.Admin.Controllers
{
  [Area("Owner")]
  public class AdminController : Controller
  {
    private readonly ApplicationDbContext _db;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminController(ApplicationDbContext db, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
      _db = db;
      _roleManager = roleManager;
      _userManager = userManager;
    }

    public IActionResult Index()
    {
      return View();
    }

    public IActionResult Dashboard()
    {
      IEnumerable<Product> products = _db.Products.Include("Category").ToList();
      return View(products);
    }

    [HttpGet]
    public IActionResult ListUser()
    {
      var users = _userManager.Users;
      return View(users);
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
        product = _db.Products.Include("Category").FirstOrDefault(product => product.IdProduct == id);
        return View(product);
      }
    }

    [HttpPost]
    public IActionResult Upsert(Product product)
    {
      if (ModelState.IsValid)
      {
        if (product.IdProduct == 0)
        {
          _db.Products.Add(product);
        }
        else
        {
          _db.Products.Update(product);
        }
        _db.SaveChanges();
        return RedirectToAction("AdminProducts");
      }
      return View();
    }

    public IActionResult Delete(int id)
    {
      var product = _db.Products.FirstOrDefault(product => product.IdProduct == id);
      if (product == null)
      {
        return NotFound();
      }
      _db.Products.Remove(product);
      _db.SaveChanges();

      return RedirectToAction("AdminProducts");
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
    public IActionResult AdminProducts(int page = 1)
    {
      //page = page < 1 ? 1 : page;
      //int pagesize = 12;
      //IEnumerable<Product> products = _db.Products.Include("Category").ToList().ToPagedList(page, pagesize);
      //return View(products);

      IEnumerable<Product> products = _db.Products.Include("Category").ToList();
      const int pageSize = 12;
      page = page < 1 ? 1 : page;
      int recsCount = products.Count();
      var pager = new Pager(recsCount, page, pageSize);
      int recSkip = (page - 1) * pageSize;
      var data = products.Skip(recSkip).Take(pager.PageSize).ToList();
      this.ViewBag.Pager = pager;
      return View(data);
    }

    public IActionResult AddProducts()
    {
      return View();
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

  }
}
