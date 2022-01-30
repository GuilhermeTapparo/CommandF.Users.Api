using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandF.Users.API
{
    public static class EnvironmentConfig
    {
        public static class MongoDb
        {
            public static string ConnectionString { get { return GetVariableOrDefault("MongoDb.ConnectionString", "mongodb://root:5oPHY9A79y@localhost:27017/?authSource=admin&readPreference=primary&appname=UsersApi&ssl=false"); } }
        }

        public static class Credentials
        {
            public static string Host { get { return GetVariableOrDefault("Credentials.Host", "https://localhost:8444/consumers/frontend/basic-auth"); } }
        }

        private static string GetVariableOrDefault(string variable, string @default = "")
        {
            var value = Environment.GetEnvironmentVariable(variable);

            if (String.IsNullOrEmpty(value))
                return @default;

            return value;
        }

    }
}
