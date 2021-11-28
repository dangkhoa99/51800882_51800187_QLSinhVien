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
        [DisplayName("Điểm")]
        public int Diem { get; set; }

        [Required]
        [DisplayName("Mã môn học")]
        public string MaMH { get; set; }

        [ForeignKey("MaMH")]
        public virtual MonHoc MonHocs { get; set; }

        [Required]
        [DisplayName("Mã SV")]
        public string MaSV { get; set; }

        [ForeignKey("MaSV")]
        public virtual SinhVien SinhViens { get; set; }
    }
}