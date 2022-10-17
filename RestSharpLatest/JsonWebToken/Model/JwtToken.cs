using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestSharpLatest.JsonWebToken.Model
{
    public class JwtToken
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
