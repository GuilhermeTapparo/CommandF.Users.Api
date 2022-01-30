using CommandF.Users.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandF.Users.API.Services.Users
{
    public interface IUserService
    {
        Task<User> CreateUser(Guid requestId, User user);
    }
}
