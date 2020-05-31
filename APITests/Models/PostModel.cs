using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace APITests.Models
{
    [JsonObject]
    public class PostModel
    {
        [JsonProperty] public string userId { get; set; }
        [JsonProperty] public string id { get; set; }
        [JsonProperty] public string title { get; set; }
        [JsonProperty] public string body { get; set; }
    }
}
