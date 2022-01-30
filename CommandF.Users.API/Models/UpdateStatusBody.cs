using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandF.Users.API.Models
{
    public class UpdateStatusBody
    {
        [JsonProperty("newStatus", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public UserStatus NewStatus { get; set; }
    }
}
