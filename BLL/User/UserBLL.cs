using WebWarehouse.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWarehouse.Model;

namespace WebWarehouse.BLL
{
    public class UserBLL : IUserBLL
    {

        private IUserRepository db;

        public UserBLL()
        {
            db = new UserRepository();
        }

        public UserBLL(IUserRepository stub)
        {
            db = stub;
        }


        public bool Create(User user)
        {
            return db.Create(user);
        }

        public bool Delete(int id)
        {

            return db.Delete(id);
        }

        public User Find(int id)
        {
        
            return db.Find(id);
        }

        public bool Update(User user)
        {
            return db.Update(user);
        }

        public ICollection<User> FindAll()
        {
            return db.FindAll();
        }

        public User existingUser(User user)
        {
            return db.existingUser(user);
        }

        public Order getFirstOrderByStatus(int userID, OrderEnum orderEnum)
        {
            return db.getFirstOrderByStatus(userID, orderEnum);
        }
        
        public Order ActiveOrder(int userId)
        {
            return db.ActiveOrder(userId);
        }
    }
}
