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
    public class MonHoc
    {
        [Key]
        [Required(ErrorMessageResourceType = typeof(LangResource), ErrorMessageResourceName = "messRequiredSubjectId")]
        [StringLength(5, ErrorMessageResourceType = typeof(LangResource), ErrorMessageResourceName = "messStringSubjectId")]
        [Display(Name = "subjectId", ResourceType = typeof(LangResource))]
        public string MaMH { get; set; }

        [Required(ErrorMessageResourceType = typeof(LangResource), ErrorMessageResourceName = "messRequiredSubjectName")]
        [StringLength(100, ErrorMessageResourceType = typeof(LangResource), ErrorMessageResourceName = "messStringSubjectName")]
        [Display(Name = "subjectName", ResourceType = typeof(LangResource))]
        public string TenMH { get; set; }

        [Required(ErrorMessageResourceType = typeof(LangResource), ErrorMessageResourceName = "messRequiredNumberLess")]
        [Range(1, Int32.MaxValue, ErrorMessageResourceType = typeof(LangResource), ErrorMessageResourceName = "messStringNumberLess")]
        [Display(Name = "numberLessons", ResourceType = typeof(LangResource))]
        public int SoTiet { get; set; }

        [Required(ErrorMessageResourceType = typeof(LangResource), ErrorMessageResourceName = "messRequiredFaculty")]
        [Display(Name = "facultyId", ResourceType = typeof(LangResource))]
        public string MaKhoa { get; set; }

        [ForeignKey("MaKhoa")]
        public virtual Khoa Khoa { get; set; }

        [JsonIgnore]
        public virtual ICollection<KetQua> KetQuas { get; set; }
    }
}