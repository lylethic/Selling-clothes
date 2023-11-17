namespace ClothesWeb.Models
{
  public class CartViewModel
  {
    public IEnumerable<Cart> listCart { get; set; }
    public HoaDon HoaDon { get; set; }

    public IEnumerable<ChiTietHoaDon> listChiTiet { get; set; }
    public ChiTietHoaDon ChiTietHoaDon { get; set; }
  }
}
