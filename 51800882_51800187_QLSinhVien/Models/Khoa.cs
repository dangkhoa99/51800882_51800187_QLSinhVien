using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _51800882_51800187_QLSinhVien.Models
{
    public class Khoa
    {
        [Key]
        [Required(ErrorMessage = "Vui lòng nhập Mã Khoa")]
        [StringLength(5, ErrorMessage = "Mã Khoa không vượt quá 5 kí tự")]
        [DisplayName("Mã Khoa")]
        public string MaKhoa { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Tên Khoa")]
        [StringLength(100, ErrorMessage = "Mã Khoa không vượt quá 100 kí tự")]
        [DisplayName("Tên Khoa")]
        public string TenKhoa { get; set; }

        [JsonIgnore]
        public virtual ICollection<SinhVien> SinhViens { get; set; }
    }
}