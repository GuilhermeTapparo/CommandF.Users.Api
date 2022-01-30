using CommandF.Users.API.Exceptions;
using CommandF.Users.API.Models;
using CommandF.Users.API.Repositories.Users.Index;
using CommandF.Users.API.Utils;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandF.Users.API.Repositories.Users
{
    public class UsersRepository : IUsersRepository
    {
        private IMongoCollection<User> userCollection;
        private IMongoCollection<EnvironmentData> envCollection;
        private IMongoCollection<NewsletterUser> newsletterCollection;

        private IIndexManager indexManager;

        public UsersRepository(IMongoClient mongoClient, IIndexManager indexManager)
        {
            var client = mongoClient ?? throw new ArgumentNullException(nameof(mongoClient));
            this.indexManager = indexManager ?? throw new ArgumentNullException(nameof(indexManager));

            this.indexManager.EnsureIndexesExistence();
            this.envCollection = client.GetDatabase("Users").GetCollection<EnvironmentData>("Environment");
            this.userCollection = client.GetDatabase("Users").GetCollection<User>("Users");
            this.newsletterCollection = client.GetDatabase("Users").GetCollection<NewsletterUser>("Newsletter");
        }

        public async Task<string> AddEnvironment(string environment, string data)
        {
            // await FlushEnv();
            var document = new EnvironmentData(environment, data);
            await envCollection.InsertOneAsync(document);
            return document.Id.ToString();
        }

        // private async Task FlushEnv()
        // {
        //     await envCollection.DeleteManyAsync(Builders<EnvironmentData>.Filter.Empty);
        // }

        public async Task<NewsletterUser> AddToNewsletter(string email, string name)
        {
            var document = new NewsletterUser(email, name);
            await newsletterCollection.InsertOneAsync(document);
            return document;
        }

        public async Task<User> CreateUser(User user)
        {
            user.Id = Guid.NewGuid();
            user.CreatedAt = BrazilDateTime.GetCurrentDate();

            await userCollection.InsertOneAsync(user);
            return user;
        }

        public async Task<User> GetByEmail(string email)
        {
            var userFilter = Builders<User>.Filter.Eq(_ => _.Email, email);
            var docs = await userCollection.FindAsync(userFilter);
            var userDoc = docs.FirstOrDefault();

            if (userDoc == null)
                throw new NotFoundException($"User {email} not found");
            return userDoc;
        }

        public async Task<User> GetByUsername(string username)
        {
            var userFilter = Builders<User>.Filter.Eq(_ => _.Username, username);
            var docs = await userCollection.FindAsync(userFilter);
            var userDoc = docs.FirstOrDefault();

            if (userDoc == null)
                throw new NotFoundException($"User {username} not found");
            return userDoc;
        }

        public async Task<EnvironmentData> GetEnvironmentData(string environment, string id)
        {
            var envFilter = Builders<EnvironmentData>.Filter.Eq(_ => _.Environment, environment);

            if(id.ToLowerInvariant() == "latest".ToLowerInvariant())
            {
                var sort = Builders<EnvironmentData>.Sort.Descending(_ => _.CreatedAt);
                return (await envCollection.FindAsync(envFilter, new FindOptions<EnvironmentData,EnvironmentData>()
                {
                    Sort = sort
                })).FirstOrDefault();
            }

            var idFilter = Builders<EnvironmentData>.Filter.Eq(_ => _.Id, Guid.Parse(id));
            var completeFilter = Builders<EnvironmentData>.Filter.And(envFilter, idFilter);
            var docs = await envCollection.FindAsync(idFilter);
            var envDoc = docs.FirstOrDefault();

            if (envDoc == null)
                throw new NotFoundException($"Environment {id} not found");
            return envDoc;
        }

        public async Task UpdateStatus(string username, UserStatus newStatus)
        {
            var statusUpdate = Builders<User>.Update.Set(_ => _.Status, newStatus);
            await userCollection.FindOneAndUpdateAsync(_ => _.Username == username, statusUpdate);
        }

        public Task<User> UpdateUser(string username, User user)
        {
            var updateDefination = new List<UpdateDefinition<User>>();
            updateDefination.Add(Builders<User>.Update.Set("Email", user.Email));
            updateDefination.Add(Builders<User>.Update.Set("Bio", user.Bio));
            updateDefination.Add(Builders<User>.Update.Set("Degree", user.Degree));
            updateDefination.Add(Builders<User>.Update.Set("Birthdate", user.Birthdate));
            updateDefination.Add(Builders<User>.Update.Set("Phone", user.Phone));
            updateDefination.Add(Builders<User>.Update.Set("Company", user.Company));
            updateDefination.Add(Builders<User>.Update.Set("Role", user.Role));
            updateDefination.Add(Builders<User>.Update.Set("LivedCities", user.LivedCities));
            updateDefination.Add(Builders<User>.Update.Set("PortfolioUrl", user.PortfolioUrl));
            updateDefination.Add(Builders<User>.Update.Set("CurriculumUrl", user.CurriculumUrl));
            updateDefination.Add(Builders<User>.Update.Set("Address", user.Address));

            var combinedUpdate = Builders<User>.Update.Combine(updateDefination);

            userCollection.UpdateOneAsync(_ => _.Username == username, combinedUpdate);
            return this.GetByUsername(username);
        }
    }
}
