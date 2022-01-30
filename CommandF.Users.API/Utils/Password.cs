using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandF.Users.API.Utils
{
    public class Password
    {
        public static string BuildHash(string password)
        {
            var passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }
    }
}
