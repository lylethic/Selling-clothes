using Azure;
using ClothesWeb.Data;
using ClothesWeb.Migrations;
using ClothesWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothesWeb.Areas.Owner.Controllers
{
  [Area("Owner")]
  public class DonHangController : Controller
  {
    private readonly ApplicationDbContext _db;

    public DonHangController(ApplicationDbContext db)
    {
      _db = db;
    }

    public IActionResult Index(int page = 1)
    {
      IEnumerable<HoaDon> hoadons = _db.HoaDons.Include(x => x.User).ToList();
      const int pageSize = 12;
      page = page < 1 ? 1 : page;
      int recsCount = hoadons.Count();
      var pager = new Pager(recsCount, page, pageSize);
      int recSkip = (page - 1) * pageSize;
      var data = hoadons.Skip(recSkip).Take(pager.PageSize).ToList();
      this.ViewBag.Pager = pager;
      return View(data);
    }

    public IActionResult DonHangDetail(int donHangId)
    {
      var detailsOrder = _db.ChiTietHoaDons.
        Include(od => od.Product)
        .Where(od => od.HoaDonId == donHangId)
        .ToList();
      return View(_db.HoaDons.OrderByDescending(p => p.Id).ToList());
    }

    public IActionResult XacNhan(int donHangId)
    {
      var hoadon = _db.HoaDons.FirstOrDefault(x => x.Id == donHangId);
      if (hoadon != null)
      {
        hoadon.OrderStatus = "Đã xác nhận";
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public IActionResult DonhangDetailsAdmin(int id)
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

    public IActionResult DonhangDeleteAdmin(int id)
    {
      var donhangDelete = _db.HoaDons.FirstOrDefault(c => c.Id == id);
      if (donhangDelete == null)
      {
        return NotFound();
      }
      _db.HoaDons.Remove(donhangDelete);
      _db.SaveChanges();

      return RedirectToAction("Index");
    }
  }
}
