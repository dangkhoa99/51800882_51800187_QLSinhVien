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
    public class KetQua
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int STT { get; set; }
        [Required]
        [DisplayName("Lần thi")]
        public int LanThi { get; set; }
        [DisplayName("Điểm")]
        public int Diem { get; set; }
        [DisplayName("Mã môn học")]
        public string MaMH { get; set; }
        [ForeignKey("MaMH")]
        
        public virtual MonHoc MonHocs { get; set; }
        [DisplayName("Mã SV")]
        public string MaSV { get; set; }
        [ForeignKey("MaSV")]
        
        public virtual SinhVien SinhViens { get; set; }
    }
}