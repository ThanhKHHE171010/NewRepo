using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAL
{
    public class LoginDAO
    {
        public Customer FindCus(string username,string password)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                return context.Customers.FirstOrDefault(c => c.Email == username && c.Password == password);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public User FindUser(string email, string password)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                return context.Users.FirstOrDefault(u => u.UserName == email && u.Password == password);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public User FindUserByUsername(string username)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                return context.Users.FirstOrDefault(u => u.UserName == username);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Customer FindCusByUsername(string username)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                return context.Customers.FirstOrDefault(u => u.Email == username);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }




        public bool AddCus(Customer customer)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                context.Customers.Add(customer);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
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
                return false;
            }
        }

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
                return false;
            }
        }

        public bool UpdateCus(Customer user)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                context.Customers.Update(user);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
