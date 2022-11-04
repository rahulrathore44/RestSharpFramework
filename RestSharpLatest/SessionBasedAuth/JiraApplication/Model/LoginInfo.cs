using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestSharpLatest.SessionBasedAuth.JiraApplication.Model
{
    public class LoginInfo
    {
        [JsonPropertyName("failedLoginCount")]
        public int failedLoginCount { get; set; }
        [JsonPropertyName("loginCount")]
        public int loginCount { get; set; }
        [JsonPropertyName("lastFailedLoginTime")]
        public string lastFailedLoginTime { get; set; }
        [JsonPropertyName("previousLoginTime")]
        public string previousLoginTime { get; set; }
    }
}
