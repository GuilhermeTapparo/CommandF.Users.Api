using CommandF.Users.API.Utils;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandF.Users.API.Models
{
    public class NewsletterUser
    {
        public NewsletterUser(string email, string name)
        {
            this.Id = Guid.NewGuid();
            this.Email = email;
            this.Name = name;
            this.CreatedAt = BrazilDateTime.GetCurrentDate();
        }

        [BsonId]
        public Guid Id { get; set; }

        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name {get; set;}

        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreatedAt { get; set; }
    }
}
