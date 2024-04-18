namespace ClothesWeb.Models
{
  public class Charts
  {
  }

  public class ChartData
  {
    public int Month { get; set; }  // Month number (1-12)
    public int Year { get; set; }
    public double TotalSales { get; set; }  // Total sales for the month
  }

  public class ChartViewModel
  {
    public List<ChartData> ChartData { get; set; }
  }

}
