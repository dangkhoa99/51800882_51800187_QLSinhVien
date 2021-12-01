using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _51800882_51800187_QLSinhVien.Models
{
    public class MyUserDAO
    {
        private QLSVContext db;

        public MyUserDAO(QLSVContext context)
        {
            this.db = context;
        }

        public bool CheckUser(MyUser user)
        {
            var myUser = db.Users.FirstOrDefault(u => (u.userName == user.userName && u.password == user.password));
            if (myUser == null)
                return false;
            return true;
        }

        public List<MyUser> GetAllUser()
        {
            return db.Users.ToList();
        }
    }
}