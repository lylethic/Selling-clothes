namespace ClothesWeb.Models
{
  public class DonHang
  {
    public int OrderId { get; set; }
    public string ImageUrl { get; set; }
    public string UserName { get; set; }
    public double TotalPrice { get; set; }
    public string Status { get; set; }
    public DateTime OrderDate { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public double ProductPrice { get; set; }
  }
}
