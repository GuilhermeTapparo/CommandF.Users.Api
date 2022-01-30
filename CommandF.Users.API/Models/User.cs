using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandF.Users.API.Models
{
    public class User
    {
        [BsonId]
        public Guid Id { get; set; }

        [JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; set; }

        [JsonProperty("firstName", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }

        [JsonProperty("lastName", NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }

        [JsonProperty("pictureUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string PictureUrl { get; set; }

        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty("password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public UserStatus Status { get; set; }

        [JsonProperty("bio", NullValueHandling = NullValueHandling.Ignore)]
        public string Bio { get; set; }

        [JsonProperty("degree", NullValueHandling = NullValueHandling.Ignore)]
        public string Degree { get; set; }

        [JsonProperty("birthdate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Birthdate { get; set; }

        [JsonProperty("phone", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }

        [JsonProperty("company", NullValueHandling = NullValueHandling.Ignore)]
        public string Company { get; set; }

        [JsonProperty("role", NullValueHandling = NullValueHandling.Ignore)]
        public string Role { get; set; }

        [JsonProperty("livedCities", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> LivedCities { get; set; }

        [JsonProperty("portfolioUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string PortfolioUrl { get; set; }

        [JsonProperty("curriculumUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string CurriculumUrl { get; set; }

        [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
        public Address Address { get; set; }

        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreatedAt { get; set; }
    }

    public class Address
    {
        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }

        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }

        [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }
    }

    public enum UserStatus
    {
        Inactive = 0,
        Active = 1
    }
}
