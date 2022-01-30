using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandF.Users.API.Services.Credentials
{
    public interface ICredentialsService
    {
        Task<string> CreateCredential(Guid requestId, string username, string password);
    }
}
