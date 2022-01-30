using CommandF.Users.API.Utils;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;

namespace CommandF.Users.API.Models
{
    public class EnvironmentData
    {
        public EnvironmentData(string environment, string data)
        {
            this.Id = Guid.NewGuid();
            this.Data = data;
            this.Environment = environment;
            this.CreatedAt = BrazilDateTime.GetCurrentDate();
        }

        [BsonId]
        public Guid Id { get; set; }
        
        [JsonProperty("environment", NullValueHandling = NullValueHandling.Ignore)]
        public string Environment { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public string Data { get; set; }

        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreatedAt { get; set; }

    }
}