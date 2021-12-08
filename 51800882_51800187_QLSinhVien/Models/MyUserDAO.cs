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
            u.roles = "user";

            var existUserName = db.Users.Where(us => us.userName == u.userName).FirstOrDefault();
            if (existUserName != null)
                return false;

            db.Users.Add(u);
            db.SaveChanges();
            return true;
        }

        public bool ExistUserName(string userName)
        {
            var existUserName = db.Users.Where(us => us.userName == userName).FirstOrDefault();
            if (existUserName != null)
                return true;
            return false;
        }

        public bool EditUser(MyUser u)
        {
            var oldUser = db.Users.FirstOrDefault(m => m.id == u.id);
            if (oldUser == null)
                return false;

            oldUser.password = u.password;

            db.SaveChanges();
            return true;
        }

        public bool DeleteUser(int id)
        {
            var u = db.Users.FirstOrDefault(m => m.id == id);
            if (u == null)
                return false;

            db.Users.Remove(u);
            db.SaveChanges();
            return true;
        }

        public MyUser GetByID(int id)
        {
            return db.Users.FirstOrDefault(m => m.id == id);
        }
    }
}