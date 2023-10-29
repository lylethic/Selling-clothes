using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace ClothesWeb.Models
{
  public class Category
  {
    [Key]
    public int LoaiId { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên loại sản phẩm!")]
    [Display(Name = "Thể loại")]
    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập đúng định dạng ngày hoặc chọn ngày!")]
    [Display(Name = "Ngày tạo")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy}")]
    public DateTime? DateCreated { get; set; } = DateTime.Now;
  }
}
