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

        public MyUser CheckUser(MyUser user)
        {
            var myUser = db.Users.FirstOrDefault(u => (u.userName == user.userName && u.password == user.password));
            return myUser;
        }

        public List<MyUser> GetAllUser()
        {
            return db.Users.ToList();
        }

        public bool AddUser(MyUser u)
        {
            db.Users.Add(u);
            db.SaveChanges();
            return true;
        }
    }
}