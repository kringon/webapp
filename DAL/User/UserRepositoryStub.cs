using System;
using System.Collections.Generic;
using WebWarehouse.Model;

namespace WebWarehouse.DAL
{
    public class UserRepositoryStub : IUserRepository
    {
        public Order ActiveOrder(int userId)
        {
            if (userId == 0)
            {
                return null;
            }
            else
            {

               Order order = new Order()
                {
                    ID = userId
                };
               return order;
            }
        }

        //Create a new User from a bound User
        public bool Create(User user)
        {
            if (user.ID == 0)
                return false;
            else
                return true;
        }

        //Delete an User by ID
        public bool Delete(int id)
        {
            if (id == 0)
                return false;
            else
                return true;
        }

        public User existingUser(User user)
        {
            if (user.ID == 4)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        //Find an UserCategory by ID
        public User Find(int id)
        {
            if (id == 0)
            {
                return null;
            }
            else
            {
                List<Order> orders = new List<Order>();
                User user = new User()
                {
                    ID = id,
                    Orders = orders,
                };

                return user;
            }
        }

        public ICollection<User> FindAll()
        {
            List<User> users = new List<User>
            {
                new User{Username="sdfg",Password=hash("sdfg"),Address="sdfg",  Role = UserRole.Admin},
                new User{Username="wert",Password=hash("wert"),Address="wert", Role = UserRole.Customer},
            };

            return users;
        }

        public Order getFirstOrderByStatus(int userID, OrderEnum orderEnum)
        {
            if (userID != 0 && orderEnum != null)
            {
                Order order = new Order()
                {
                    ID = userID
                };

                return order;
            }
            else
            {
                return null;
            }
        }

        //Update a specific UserCategory
        public bool Update(User user)
        {
            if (user.ID == 0)
                return false;
            else
                return true;
        }
        private String hash(string password)
        {
            var algoritme = System.Security.Cryptography.SHA256.Create();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            return Convert.ToBase64String(algoritme.ComputeHash(data));
        }
    }
}