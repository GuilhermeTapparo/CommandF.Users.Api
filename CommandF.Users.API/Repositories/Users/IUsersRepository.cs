using CommandF.Users.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandF.Users.API.Repositories.Users
{
    public interface IUsersRepository
    {
        Task<User> GetByUsername(string username);
        Task<User> GetByEmail(string email);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(string username, User user);
        Task UpdateStatus(string username, UserStatus newStatus);
        Task<NewsletterUser> AddToNewsletter(string email, string name);
        Task<EnvironmentData> GetEnvironmentData(string environment, string id);
        Task<string> AddEnvironment(string environment, string data);
    }
}
