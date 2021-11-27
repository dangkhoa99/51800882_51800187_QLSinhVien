using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _51800882_51800187_QLSinhVien.Models
{
    public class KetQua
    {
        [Key]
        public int LanThi { get; set; }
        public int Diem { get; set; }
        [JsonIgnore]
        public virtual MonHoc MonHocs { get; set; }
        [JsonIgnore]
        public virtual SinhVien SinhViens { get; set; }
    }
}