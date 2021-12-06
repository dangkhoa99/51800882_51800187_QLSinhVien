using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace _51800882_51800187_QLSinhVien.Models
{
    public class MyUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Tài khoản")]
        [StringLength(50, ErrorMessage = "Tài khoản không vượt quá 50 kí tự")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu")]
        [StringLength(50, ErrorMessage = "Mật khẩu không vượt quá 50 kí tự")]
        public string password { get; set; }

        [StringLength(50)]
        public string roles { get; set; }

        [DisplayName("Mã giảng viên")]
        public string MaGV { get; set; }

        [ForeignKey("MaGV")]
        public virtual GiangVien GiangViens { get; set; }
    }
}