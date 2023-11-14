using Azure;
using ClothesWeb.Data;
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
      ChiTietHoaDon chiTietDonHang = new ChiTietHoaDon()
      {
        Id = donHangId,
        HoaDon = _db.HoaDons.Include(x => x.User).FirstOrDefault(User => User.Id == donHangId),
      };
      return View(chiTietDonHang);
    }
  }
}
