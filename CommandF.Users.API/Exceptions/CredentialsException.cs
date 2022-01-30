using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CommandF.Users.API.Exceptions
{
    public class CredentialsException : Exception
    {
        public CredentialsException()
        {
        }

        public CredentialsException(string message) : base(message)
        {
        }

        public CredentialsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CredentialsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
