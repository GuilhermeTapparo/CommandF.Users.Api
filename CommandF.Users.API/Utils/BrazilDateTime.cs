using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandF.Users.API.Utils
{
    public static class BrazilDateTime
    {
        public static DateTime GetCurrentDate()
        {
            return DateTime.UtcNow.AddHours(-3);
        }
    }
}
