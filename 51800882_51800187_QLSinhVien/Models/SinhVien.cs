﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _51800882_51800187_QLSinhVien.Models
{
    public class SinhVien
    {
        [Key]
        [Required]
        [StringLength(5)]
        [DisplayName("Mã SV")]
        public string MaSV { get; set; }
        [Required]
        [StringLength(100)]
        [DisplayName("Họ Tên")]
        public string HoTen { get; set; }
        [DisplayName("Ngày Sinh")]
        public DateTime NgaySinh { get; set; }
        [DisplayName("Giới tính")]
        public string GioiTinh { get; set; }
        [JsonIgnore]
        public virtual Khoa Khoa { get; set; }
        [JsonIgnore]
        public virtual ICollection<KetQua> KetQuas { get; set; }
    }
}