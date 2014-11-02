using WebWarehouse.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWarehouse.Model;

namespace WebWarehouse.BLL
{
    public class UserBLL
    {
        private UserDAL db = new UserDAL();

        public bool Create(User User)
        {
            return db.Create(User);
        }

        public bool Delete(int id)
        {

            return db.Delete(id);
        }

        public User Find(int? id)
        {
        
            return db.Find(id);
        }

        public bool Update(User User)
        {
            return db.Update(User);
        }

        public ICollection<User> FindAll()
        {
            return db.FindAll();
        }

        public User existingUser(User user)
        {
            return db.existingUser(user);
        }

        public Order getFirstOrderByStatus(int? userID, OrderEnum orderEnum)
        {
            return db.getFirstOrderByStatus(userID, orderEnum);
        }

      


    }
}
