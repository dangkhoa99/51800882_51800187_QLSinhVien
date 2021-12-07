using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using _51800882_51800187_QLSinhVien.Res;

namespace _51800882_51800187_QLSinhVien.Models
{
    public class KetQua
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int STT { get; set; }

        [Required(ErrorMessageResourceType = typeof(LangResource), ErrorMessageResourceName = "messRequiredScore")]
        [Range(0, 10, ErrorMessageResourceType = typeof(LangResource), ErrorMessageResourceName = "messStringScore")]
        [Display(Name = "score", ResourceType = typeof(LangResource))]
        public int Diem { get; set; }

        [Required(ErrorMessageResourceType = typeof(LangResource), ErrorMessageResourceName = "messRequiredSubject")]
        [Display(Name = "subjectId", ResourceType = typeof(LangResource))]
        public string MaMH { get; set; }

        [ForeignKey("MaMH")]
        public virtual MonHoc MonHocs { get; set; }

        [Required(ErrorMessageResourceType = typeof(LangResource), ErrorMessageResourceName = "messRequiredStudent")]
        [Display(Name = "studentId", ResourceType = typeof(LangResource))]
        public string MaSV { get; set; }

        [ForeignKey("MaSV")]
        public virtual SinhVien SinhViens { get; set; }
    }
}