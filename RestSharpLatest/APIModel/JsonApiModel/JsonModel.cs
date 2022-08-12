using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestSharpLatest.APIModel.JsonApiModel
{
    public class JsonModel
    {
        [JsonPropertyName("BrandName")]
        public string BrandName { get; set; }
        [JsonPropertyName("Features")]
        public Features Features { get; set; }
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("LaptopName")]
        public string LaptopName { get; set; }
    }
}
