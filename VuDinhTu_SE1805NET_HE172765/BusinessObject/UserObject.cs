using DataAccess.DAL;
using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace BusinessObject
{
    public class UserObject
    {
        private readonly UserDAO _userDAO;
       

        public UserObject()
        {
            _userDAO = new UserDAO();
        }

        // Get All Users
        public List<User> GetAllUsers()
        {
            try
            {
                return _userDAO.GetAllUsers();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Get User by ID
        public User GetUserById(int id)
        {
            try
            {
                return _userDAO.GetUserById(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Add User
        public bool AddUser(User user)
        {
            try
            {
                return _userDAO.AddUser(user);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Update User
        public bool UpdateUser(User user)
        {
            try
            {
                return _userDAO.UpdateUser(user);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Delete User
        public bool DeleteUser(int id)
        {
            try
            {
                return _userDAO.DeleteUser(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
