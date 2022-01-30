using CommandF.Users.API.Exceptions;
using CommandF.Users.API.Utils;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CommandF.Users.API.Services.Credentials
{
    public class CredentialsService : ICredentialsService
    {
        private readonly ILogger<CredentialsService> logger;
        private readonly IRestClient client;

        public CredentialsService(ILogger<CredentialsService> logger, IRestClient client)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.client.BaseUrl = new Uri(EnvironmentConfig.Credentials.Host);
            ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
        }

        public async Task<string> CreateCredential(Guid requestId, string username, string password)
        {
            try
            {
                this.logger.LogInformation($"[{requestId} - {BrazilDateTime.GetCurrentDate()}] Starting to create credential {username} {password}");
                var pwdHash = Password.BuildHash(password);

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("username", username);
                request.AddParameter("password", pwdHash);
                IRestResponse response = await client.ExecuteAsync(request);

                if (!response.IsSuccessful)
                    throw new CredentialsException($"{response.StatusCode} {response.Content}");

                return pwdHash;
            }
            catch (Exception e)
            {
                this.logger.LogError($"[{requestId} - {BrazilDateTime.GetCurrentDate()}] Failed to create credential for user {username} {password}: {e.Message} {e.StackTrace}");
                throw new CredentialsException($"Failed to create credential for user {username}: {e.Message}");
            }
        }
    }
}