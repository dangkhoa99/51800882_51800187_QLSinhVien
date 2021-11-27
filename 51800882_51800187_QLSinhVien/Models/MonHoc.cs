using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _51800882_51800187_QLSinhVien.Models
{
    public class MonHoc
    {
        [Key]
        [StringLength(5)]
        [DisplayName("Mã môn học")]
        public string MaMH { get; set; }
        [Required]
        [StringLength(100)]
        [DisplayName("Tên môn học")]
        public string TenMH { get; set; }
        [DisplayName("Số tiết")]
        public int SoTiet { get; set; }
        [JsonIgnore]
        public virtual ICollection<KetQua> KetQuas { get; set; }
    }
}