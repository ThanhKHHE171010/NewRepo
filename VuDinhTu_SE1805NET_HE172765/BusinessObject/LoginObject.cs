using BusinessObject.Utils;
using DataAccess.DAL;
using DataAccess.Models;
using System;

namespace BusinessObject
{
    public class LoginObject
    {
        private readonly LoginDAO _loginDAO;
        public static User? Account { get; set; }
        public static Customer? AccCustomer { get; set; }
        public LoginObject()
        {
            _loginDAO = new LoginDAO();
        }
        public User userACC()
        {
            
            return Account;
        }
        public Customer cusACC()
        {
            
            return AccCustomer;
        }
        
        public bool Login(string username, string password)
        {
            string hashedPassword = PasswordHelper.HashPassword(password);
            User user = _loginDAO.FindUser(username, hashedPassword);
            if (user == null || user.Status != "Active")
            {
                return false;
            }
            SessionManager.CreateSession(user);
            Account = user;
            return true;
        }

        public bool LoginCus(string username, string password)
        {
            string hashedPassword = PasswordHelper.HashPassword(password);
            Customer cus = _loginDAO.FindCus(username, hashedPassword);
            if (cus == null)
            {
                return false;
            }
            SessionManager.CreateSessionCus(cus);
            AccCustomer = cus;
            return true;
        }



        public void Logout()
        {
            SessionManager.DestroySession();
        }

        public bool Register(string username, string password, string displayName, string status)
        {
            if (!Validation.ValidateEmail(username))
            {
                throw new ArgumentException("Invalid email format. Must be @gmail.com.");
            }

            if (!Validation.ValidatePassword(password))
            {
                throw new ArgumentException("Password must be at least 6 characters long.");
            }

            if (_loginDAO.FindUserByUsername(username) != null)
            {
                return false;
            }
            if (_loginDAO.FindCusByUsername(username) != null)
            {
                return false;
            }
            string hashedPassword = PasswordHelper.HashPassword(password);

            User newUser = new User
            {
                UserName = username,
                Password = hashedPassword,
                DisplayName = displayName,
                Status = "Inactive",
                IdRole = 2
            };

            return _loginDAO.AddUser(newUser);
        }

        public bool RegisterCus(string username, string password, string displayName,string phone,string address)
        {
            if (!Validation.ValidateEmail(username))
            {
                throw new ArgumentException("Invalid email format. Must be @gmail.com.");
            }

            if (!Validation.ValidatePassword(password))
            {
                throw new ArgumentException("Password must be at least 6 characters long.");
            }

            if (_loginDAO.FindCusByUsername(username) != null)
            {
                return false;
            }
            if (_loginDAO.FindUserByUsername(username) != null)
            {
                return false;
            }

            string hashedPassword = PasswordHelper.HashPassword(password);

            Customer newUser = new Customer
            {
                DisplayName = displayName,
                Address = address,
                Phone = phone,
                Email = username,
                Password = hashedPassword
            };

            return _loginDAO.AddCus(newUser);
        }

        public bool ResetPasswordCus(string username, string newPassword)
        {
            if (!Validation.ValidatePassword(newPassword))
            {
                throw new ArgumentException("Password must be at least 6 characters long.");
            }

            Customer user = _loginDAO.FindCusByUsername(username);
            if (user == null)
            {
                return false; // User does not exist
            }

            user.Password = PasswordHelper.HashPassword(newPassword);
            return _loginDAO.UpdateCus(user);
        }


        public bool ResetPassword(string username, string newPassword)
        {
            if (!Validation.ValidatePassword(newPassword))
            {
                throw new ArgumentException("Password must be at least 6 characters long.");
            }

            User user = _loginDAO.FindUserByUsername(username);
            if (user == null)
            {
                return false; // User does not exist
            }

            user.Password = PasswordHelper.HashPassword(newPassword);
            return _loginDAO.UpdateUser(user);
        }
       
        public User FindUserByUsername(string username)
        {
            return _loginDAO.FindUserByUsername(username);
        }
        public Customer FindCusByUsername(string username)
        {
            return _loginDAO.FindCusByUsername(username);
        }
        public int GetCurrentCustomerId()
        {
            return AccCustomer?.Id ?? 0;
        }
        public int GetCurrentUserId()
        {
            return Account?.Id ?? 0;
        }
    }
}
