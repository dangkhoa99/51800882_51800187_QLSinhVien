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
        [Required]
        [StringLength(5)]
        [DisplayName("Mã Khoa")]
        public string MaKhoa { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Tên Khoa")]
        public string TenKhoa { get; set; }

        [JsonIgnore]
        public virtual ICollection<SinhVien> SinhViens { get; set; }
    }
}