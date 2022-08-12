using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestSharpLatest.APIModel.JsonApiModel
{
    public class Features
    {
        [JsonPropertyName("Feature")]
        public List<string> Feature { get; set; }
    }
}
