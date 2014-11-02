using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebWarehouse.Model;

namespace WebWarehouse.DAL
{
    public class UserRepository : IUserRepository
    {
        private WarehouseContext Db = new WarehouseContext();
        private ILog Logger = LogManager.GetLogger(typeof(UserRepository));

        //Create a new User from a bound User
        public bool Create(User User)
        {
            try
            {
                User.Password = hash(User.Password);

                Db.Users.Add(User);
                Db.SaveChanges();
                Logger.Info("Added new User");
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return false;
            }
        }

        //Delete an User by ID
        public bool Delete(int Id)
        {
            try
            {
                User User = Db.Users.Find(Id);
                Db.Users.Remove(User);
                Db.SaveChanges();
                Logger.Info("User with id: " + Id + " was deleted.");
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return false;
            }
        }

        public User existingUser(User user)
        {
            if (user.Password == null)
            {
                Logger.Debug("No password provided for existingUser check.");
                return null;
            }

            String pwToBeChecked = hash(user.Password);

            User existingUser = Db.Users.FirstOrDefault
                (b => b.Password == pwToBeChecked && b.Username == user.Username);
            if (existingUser == null)
            {
                Logger.Debug("Search for Exisiting user returned no result");
                return null;
            }
            else
            {
                Logger.Debug("Found a valid user, returning it");
                return existingUser;
            }
        }

        //Find an UserCategory by ID
        public User Find(int Id)
        {
            User Found;
            try
            {
                Logger.Info("Searching for User with id: " + Id);
                Found = Db.Users.Find(Id);
                return Found;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return null;
            }
        }

        public ICollection<User> FindAll()
        {
            try
            {
                return Db.Users.ToList();
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return null;
            }
        }

        public Order getFirstOrderByStatus(int UserID, OrderEnum orderEnum)
        {
            try
            {
                User user = Db.Users.Find(UserID);
                Order returnOrder = user.Orders.FirstOrDefault(o => o.Status == orderEnum);
                return returnOrder;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return null;
            }
        }

        //Update a specific UserCategory
        public bool Update(User User)
        {
            try
            {
                Db.Entry(User).State = EntityState.Modified;
                Db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return false;
            }
        }

        private String hash(string password)
        {
            var algoritme = System.Security.Cryptography.SHA256.Create();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            return Convert.ToBase64String(algoritme.ComputeHash(data));
        }



        public Order ActiveOrder(int userId)
        {
            Logger.Info("Entered Active ORder");
            User user = Find(userId);
            Order emptyOrder = getFirstOrderByStatus(userId, OrderEnum.Empty);
            Order browsingOrder = getFirstOrderByStatus(userId, OrderEnum.Browsing);

            if (emptyOrder == null && browsingOrder == null)
            {

                Logger.Info("No Active ORder");
                Order newOrder = new Order();
                newOrder.Items = new List<Item>();
                user.Orders.Add(newOrder);

                Update(user);

                return newOrder;
            }
            if (browsingOrder != null)
            {
                Logger.Info("Browsing ORder");
                return browsingOrder;
            }

            Logger.Info("Return Null From Active ORder");
            return null;
        }
    }
}