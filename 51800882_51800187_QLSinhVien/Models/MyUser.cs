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
    public class MyUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessageResourceType = typeof(LangResource), ErrorMessageResourceName = "messRequiredUsername")]
        [StringLength(50, ErrorMessageResourceType = typeof(LangResource), ErrorMessageResourceName = "messStringUsername")]
        [Display(Name = "username", ResourceType = typeof(LangResource))]
        public string userName { get; set; }

        [Required(ErrorMessageResourceType = typeof(LangResource), ErrorMessageResourceName = "messRequiredPassword")]
        [StringLength(50, ErrorMessageResourceType = typeof(LangResource), ErrorMessageResourceName = "messStringPassword")]
        [Display(Name = "password", ResourceType = typeof(LangResource))]
        public string password { get; set; }

        [Display(Name = "roles", ResourceType = typeof(LangResource))]
        [StringLength(50)]
        public string roles { get; set; }

        [Display(Name = "teacherId", ResourceType = typeof(LangResource))]
        public string MaGV { get; set; }

        [ForeignKey("MaGV")]
        public virtual GiangVien GiangViens { get; set; }
    }
}