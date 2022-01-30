using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandF.Users.API.Models
{
    public class ErrorResponse
    {
        public ErrorResponse(string message, string stack)
        {
            Message = message;
            Stack = stack;
        }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty("stack", NullValueHandling = NullValueHandling.Ignore)]
        public string Stack { get; set; }
    }
}
