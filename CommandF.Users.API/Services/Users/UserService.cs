using CommandF.Users.API.Exceptions;
using CommandF.Users.API.Models;
using CommandF.Users.API.Repositories.Users;
using CommandF.Users.API.Services.Credentials;
using CommandF.Users.API.Utils;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandF.Users.API.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> logger;
        private readonly ICredentialsService credentialsService;
        private readonly IUsersRepository usersRepository;

        public UserService(ILogger<UserService> logger, ICredentialsService credentialsService, IUsersRepository usersRepository)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.credentialsService = credentialsService ?? throw new ArgumentNullException(nameof(credentialsService));
            this.usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
        }

        public async Task<User> CreateUser(Guid requestId, User user)
        {
            try
            {
                this.logger.LogInformation($"[{requestId} - {BrazilDateTime.GetCurrentDate()}] Starting to create user {JsonConvert.SerializeObject(user)}");
                if (await AlreadyExistsAsync(user.Username))
                    throw new AlreadyExistsException("user already exists");

                user.Password = await this.credentialsService.CreateCredential(requestId, user.Username, user.Password);
                return await this.usersRepository.CreateUser(user);
            }
            catch(AlreadyExistsException)
            {
                throw;
            }
            catch (Exception e)
            {
                var response = new ErrorResponse(e.Message, e.StackTrace);
                this.logger.LogError($"[{requestId} - {BrazilDateTime.GetCurrentDate()}] Failed to create user {JsonConvert.SerializeObject(user)}: {e.Message} {e.StackTrace}");
                throw new Exception($"failed to create user {e.Message}");
            }
        }

        private async Task<bool> AlreadyExistsAsync(string username)
        {
            try
            {
                var user = await usersRepository.GetByUsername(username);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
