using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.DAL
{
    public class UserDAO
    {
        // Create
        public bool AddUser(User user)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                context.Users.Add(user);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Read
        public User GetUserById(int id)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                return context.Users.FirstOrDefault(u => u.Id == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Update
        public bool UpdateUser(User user)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                context.Users.Update(user);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Delete
        public bool DeleteUser(int id)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                var user = context.Users.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    context.Users.Remove(user);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // List All
        public List<User> GetAllUsers()
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                return context.Users.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
