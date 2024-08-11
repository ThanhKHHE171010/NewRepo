using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Utils
{
    public static class SessionManager
    {
        private static User? _currentUser;
        private static Customer? _customer;
        public static User? CurrentUser
        {
            get { return _currentUser; }
            private set { _currentUser = value; }
        }
        public static Customer? CurrentCus
        {
            get { return _customer; }
            private set { _customer = value; }
        }

        public static void CreateSessionCus(Customer user)
        {
            CurrentCus = user;
        }
        public static Customer GetSessionCus()
        {
            return _customer;
        }
        public static void CreateSession(User user)
        {
            CurrentUser = user;
        }

    
        public static void DestroySession()
        {
            CurrentUser = null;
        }

        public static bool IsUserLoggedIn()
        {
            return CurrentUser != null;
        }
    }
}

