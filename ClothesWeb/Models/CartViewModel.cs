namespace ClothesWeb.Models
{
  public class CartViewModel
  {
    public IEnumerable<Cart> listCart { get; set; }
    public IEnumerable<ChiTietHoaDon> DsChiTietHoaDon { get; set; }
    public HoaDon HoaDon{ get; set; }
    public DonHang DonHang { get; set; }
    //public ChiTietHoaDon ChiTietHoaDon { get; set; }
  }
}
