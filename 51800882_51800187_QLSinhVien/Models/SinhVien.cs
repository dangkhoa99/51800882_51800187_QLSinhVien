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
    public class SinhVien
    {
        [Key]
        [Required(ErrorMessageResourceType = typeof(LangResource), ErrorMessageResourceName = "messRequiredStudentId")]
        [StringLength(5, ErrorMessageResourceType = typeof(LangResource), ErrorMessageResourceName = "messStringStudentId")]
        [Display(Name = "studentId", ResourceType = typeof(LangResource))]
        public string MaSV { get; set; }

        [Required(ErrorMessageResourceType = typeof(LangResource), ErrorMessageResourceName = "messRequiredFullName")]
        [StringLength(100, ErrorMessageResourceType = typeof(LangResource), ErrorMessageResourceName = "messStringFullName")]
        [Display(Name = "fullName", ResourceType = typeof(LangResource))]
        public string HoTen { get; set; }

        [Required(ErrorMessageResourceType = typeof(LangResource), ErrorMessageResourceName = "messRequiredBirthday")]
        [Display(Name = "birthDay", ResourceType = typeof(LangResource))]
        [DataType(DataType.Date)]
        [DateValidationAttribute(ErrorMessageResourceType = typeof(LangResource), ErrorMessageResourceName = "messDateValidationAttribute")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime NgaySinh { get; set; }

        [Required(ErrorMessageResourceType = typeof(LangResource), ErrorMessageResourceName = "messRequiredGender")]
        [Display(Name = "gender", ResourceType = typeof(LangResource))]
        public string GioiTinh { get; set; }

        [Required(ErrorMessageResourceType = typeof(LangResource), ErrorMessageResourceName = "messRequiredFaculty")]
        [Display(Name = "facultyId", ResourceType = typeof(LangResource))]
        public string MaKhoa { get; set; }

        [ForeignKey("MaKhoa")]
        public virtual Khoa Khoa { get; set; }

        [JsonIgnore]
        public virtual ICollection<KetQua> KetQuas { get; set; }
    }
}