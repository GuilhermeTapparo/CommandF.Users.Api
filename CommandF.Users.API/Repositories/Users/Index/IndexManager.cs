using CommandF.Users.API.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandF.Users.API.Repositories.Users.Index
{
    public class IndexManager : IIndexManager
    {
        private readonly IMongoClient mongoClient;
        private bool ensuredIndexes;

        public IndexManager(IMongoClient mongoClient)
        {
            this.mongoClient = mongoClient ?? throw new ArgumentNullException(nameof(mongoClient));
            ensuredIndexes = false;
        }

        public void EnsureIndexesExistence()
        {
            if (ensuredIndexes)
                return;

            var db = mongoClient.GetDatabase("Users");

            var userCollection = db.GetCollection<User>("Users");
            userCollection.Indexes.CreateOne(new CreateIndexModel<User>(
                Builders<User>.IndexKeys.Ascending(_ => _.Username)));

            userCollection.Indexes.CreateOne(new CreateIndexModel<User>(
                Builders<User>.IndexKeys.Ascending(_ => _.Email)));

            userCollection.Indexes.CreateOne(new CreateIndexModel<User>(
                Builders<User>.IndexKeys.Ascending(_ => _.Role)));

            userCollection.Indexes.CreateOne(new CreateIndexModel<User>(
                Builders<User>.IndexKeys.Ascending(_ => _.Company)));


            var newsletterCollection = db.GetCollection<NewsletterUser>("Newsletter");
            newsletterCollection.Indexes.CreateOne(new CreateIndexModel<NewsletterUser>(
                Builders<NewsletterUser>.IndexKeys.Ascending(_ => _.Email)));

            var envCollection = db.GetCollection<EnvironmentData>("Environment");
            envCollection.Indexes.CreateOne(new CreateIndexModel<EnvironmentData>(
                Builders<EnvironmentData>.IndexKeys.Descending(_ => _.CreatedAt), new CreateIndexOptions
                {
                    ExpireAfter = TimeSpan.FromDays(2)
                }));

            ensuredIndexes = true;
        }
    }
}
