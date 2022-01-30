using Newtonsoft.Json;

namespace CommandF.Users.API.Models
{
    public class EnvironmentDataBody
    {

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public string Data { get; set; }
    }
}