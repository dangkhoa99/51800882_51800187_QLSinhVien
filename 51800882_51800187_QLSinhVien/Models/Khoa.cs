using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using _51800882_51800187_QLSinhVien.Res;

namespace _51800882_51800187_QLSinhVien.Models
{
    public class Khoa
    {
        [Key]
        [Required(ErrorMessageResourceType = typeof(LangResource), ErrorMessageResourceName = "messRequiredFacultyId")]
        [StringLength(5, ErrorMessageResourceType = typeof(LangResource), ErrorMessageResourceName = "messStringFacultyId")]
        [Display(Name = "facultyId", ResourceType = typeof(LangResource))]
        public string MaKhoa { get; set; }

        [Required(ErrorMessageResourceType = typeof(LangResource), ErrorMessageResourceName = "messRequiredFacultyName")]
        [StringLength(100, ErrorMessageResourceType = typeof(LangResource), ErrorMessageResourceName = "messStringFacultyName")]
        [Display(Name = "facultyName", ResourceType = typeof(LangResource))]
        public string TenKhoa { get; set; }

        [JsonIgnore]
        public virtual ICollection<SinhVien> SinhViens { get; set; }

        [JsonIgnore]
        public virtual ICollection<MonHoc> MonHocs { get; set; }
    }
}