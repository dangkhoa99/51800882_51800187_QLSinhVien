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
    public class MonHoc
    {
        [Key]
        [Required(ErrorMessage = "Vui lòng nhập Mã Môn Học")]
        [StringLength(5, ErrorMessage = "Mã Môn học không vượt quá 5 kí tự")]
        [DisplayName("Mã môn học")]
        public string MaMH { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Tên Môn Học")]
        [StringLength(100, ErrorMessage = "Tên môn học không vượt quá 100 kí tự")]
        [DisplayName("Tên môn học")]
        public string TenMH { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Số tiết học")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Số tiết học phải lớn hơn 0")]
        [DisplayName("Số tiết")]
        public int SoTiet { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn Khoa")]
        [DisplayName("Mã Khoa")]
        public string MaKhoa { get; set; }

        [ForeignKey("MaKhoa")]
        public virtual Khoa Khoa { get; set; }

        [JsonIgnore]
        public virtual ICollection<KetQua> KetQuas { get; set; }
    }
}