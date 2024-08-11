using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessObject.Utils
{
    public static class Validation
    {
        public static bool ValidateEmail(string email)
        {
            string emailPattern = @"^[\w-\.]+@gmail\.com$";
            return Regex.IsMatch(email, emailPattern);
        }

        public static bool ValidatePassword(string password)
        {
            return password.Length >= 6;
        }
    }
}
