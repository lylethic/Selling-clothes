using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesWeb.Models
{
  public class TrangThai
  {
    [Key]
    public string IdTrangThai { get; set; }

    public string TenTrangThai { get; set; } = null;

    public string? MoTa { get; set; }

    public virtual ICollection<HoaDon> HoaDon { get; set; } = new List<HoaDon>();
  }
}
