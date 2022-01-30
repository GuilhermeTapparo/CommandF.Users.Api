using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandF.Users.API.Models
{
    public class EnvironmentOutput
    {
        public EnvironmentOutput(EnvironmentData data)
        {
            this.Id = data.Id;
            this.Environment = data.Environment;
            this.CreatedAt = data.CreatedAt;
            this.Data = JsonConvert.DeserializeObject<Dictionary<string, string>>(data.Data);
        }

        public Guid Id { get; set; }

        [JsonProperty("environment", NullValueHandling = NullValueHandling.Ignore)]
        public string Environment { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Data { get; set; }

        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreatedAt { get; set; }
    }
}
