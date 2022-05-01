using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkShopClasses.UserMangement
{
    public class UserHandler
    {
        DrinkShopContext db = new DrinkShopContext();
        public List<User> GetUsers()
        {
            using (db)
            {
                return (from c in db.Users.Include(m => m.Role) select c).ToList();
            }
        }

        public User GetUser(string userName, string password)
        {
            using (db)
            {
                return (from c in db.Users.Include(m => m.Role)
                        where c.UserName.Equals(userName) && c.Password.Equals(password)
                        select c).FirstOrDefault();
            }
        }
        public User GetUserById(int id)
        {
            DrinkShopContext db = new DrinkShopContext();
            using (db)
            {
                return (from c in db.Users
                        .Include(x => x.Role)
                        where c.Id == id
                        select c).FirstOrDefault();
            }
        }
        public User GetUserByEmail(string email)
        {
            DrinkShopContext db = new DrinkShopContext();
            using (db)
            {
                return (from c in db.Users where c.Email == email select c).FirstOrDefault();
            }
        }
    }
}
