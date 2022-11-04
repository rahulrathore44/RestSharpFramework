using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestSharpLatest.SessionBasedAuth.JiraApplication.Model
{
    public class Session
    {
        [JsonPropertyName("name")]
        public string name { get; set; }
        [JsonPropertyName("value")]
        public string value { get; set; }
    }
}
