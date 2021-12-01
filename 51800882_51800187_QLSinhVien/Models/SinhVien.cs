using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace _51800882_51800187_QLSinhVien.Models
{
    public class SinhVien
    {
        [Key]
        [Required(ErrorMessage = "Vui lòng nhập Mã SV")]
        [StringLength(5, ErrorMessage = "Mã SV không vượt quá 5 kí tự")]
        [DisplayName("Mã SV")]
        public string MaSV { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Họ Tên")]
        [StringLength(100, ErrorMessage = "Họ Tên không vượt quá 100 kí tự")]
        [DisplayName("Họ Tên")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn Ngày Sinh")]
        [DisplayName("Ngày Sinh")]
        [DataType(DataType.Date)]
        [DateValidationAttribute(ErrorMessage = "Bạn đến từ tương lai à !!!")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime NgaySinh { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn Giới tính")]
        [DisplayName("Giới tính")]
        public string GioiTinh { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn Khoa")]
        [DisplayName("Mã Khoa")]
        public string MaKhoa { get; set; }

        [ForeignKey("MaKhoa")]
        public virtual Khoa Khoa { get; set; }

        [JsonIgnore]
        public virtual ICollection<KetQua> KetQuas { get; set; }
    }
}