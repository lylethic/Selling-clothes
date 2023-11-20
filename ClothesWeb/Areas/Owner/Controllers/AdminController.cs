using Azure;
using ClothesWeb.Data;
using ClothesWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using System.Net;
using System.Security.Claims;
using X.PagedList;

namespace ClothesWeb.Areas.Admin.Controllers
{
  [Area("Owner")]
  public class AdminController : Controller
  {
    private readonly ApplicationDbContext _db;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AdminController(ApplicationDbContext db, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
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

    [HttpGet]
    public IActionResult ListUser(int page = 1)
    {
      IEnumerable<ApplicationUser> users = _db.ApplicationUser;
      const int pageSize = 6;
      page = page < 1 ? 1 : page;
      int recsCount = users.Count();
      var pager = new Pager(recsCount, page, pageSize);
      int recSkip = (page - 1) * pageSize;
      var data = users.Skip(recSkip).Take(pager.PageSize).ToList();
      this.ViewBag.Pager = pager;
      return View(data);
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

    public IActionResult ViewLoaiSanPham()
    {
      var loaiSP = _db.Categories.ToList();
      ViewBag.LoaiSP = loaiSP;
      return View(loaiSP);
    }

    [HttpGet]
    public IActionResult LoaiSanPham(int id)
    {
      Category loaiSanPham = new Category();
      IEnumerable<SelectListItem> dstheloai = _db.Categories.Select(
        loaiSanPham => new SelectListItem
        {
          Value = loaiSanPham.LoaiId.ToString(),
          Text = loaiSanPham.Name,
        });

      ViewBag.Dstheloai = dstheloai;

      if (id == 0)
      {
        return View(loaiSanPham);
      }
      else
      {
        loaiSanPham = _db.Categories.FirstOrDefault(loaiSanPham => loaiSanPham.LoaiId == id);
        return View(loaiSanPham);
      }
    }

    [HttpPost]
    public IActionResult LoaiSanPham(Category category)
    {
      if (ModelState.IsValid)
      {
        if (category.LoaiId == 0)
        {
          _db.Categories.Add(category);
        }
        else
        {
          _db.Categories.Update(category);
        }
        _db.SaveChanges();
        return RedirectToAction("ViewLoaiSanPham");
      }
      return View();
    }

    public IActionResult DeleteLoaiSanPham(int id)
    {
      var category = _db.Categories.FirstOrDefault(c => c.LoaiId == id);
      if (category == null)
      {
        return NotFound();
      }
      _db.Categories.Remove(category);
      _db.SaveChanges();

      return RedirectToAction("ViewLoaiSanPham");
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

    [HttpGet]
    public IActionResult EditUser(string id)
    {
      var user = _db.ApplicationUser.FirstOrDefault(x => x.Id == id);
      return View(user);
    }

    [HttpPost]
    public IActionResult EditUser(ApplicationUser updatedUser)
    {
      var idUser = updatedUser.Id;
      var user = _db.ApplicationUser.FirstOrDefault(x => x.Id == idUser);

      if (user != null)
      {
        user.Id = idUser;
        user.Name = updatedUser.Name;
        user.Email = updatedUser.Email;
        user.PhoneNumber = updatedUser.PhoneNumber;
        user.Address = updatedUser.Address;
        _db.SaveChanges();
        return RedirectToAction("ListUser"); // Redirect to the profile page

      };
      return NotFound();
    }

    public IActionResult DeleteUser(string id)
    {
      var user = _db.ApplicationUser.FirstOrDefault(x => x.Id == id);
      if (user == null)
      {
        return NotFound();
      }
      _db.ApplicationUser.Remove(user);
      _db.SaveChanges();

      return RedirectToAction("ListUser");
    }
  }
}
