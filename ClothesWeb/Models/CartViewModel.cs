namespace ClothesWeb.Models
{
  public class CartViewModel
  {
    public IEnumerable<Cart> listCart { get; set; }
    public double TotalPrice { get; set; }
  }
}
