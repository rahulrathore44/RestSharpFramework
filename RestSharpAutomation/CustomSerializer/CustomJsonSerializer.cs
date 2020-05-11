using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization;
using RestSharp.Serialization.Json;
using RestSharpAutomation.Question.Model;

namespace RestSharpAutomation.CustomSerializer
{
    public class CustomJsonSerializer : IRestSerializer
    {
        public string Serialize(object obj) => CustomSerialize(obj);

        public string Serialize(Parameter bodyParameter) => Serialize(bodyParameter.Value);

        public T Deserialize<T>(IRestResponse response) => new JsonSerializer().Deserialize<T>(response);

        public string[] SupportedContentTypes { get; } =
        {
            "application/json", "text/json", "text/x-json", "text/javascript", "*+json"
        };

        public string ContentType { get; set; } = "application/json";

        public DataFormat DataFormat { get; } = DataFormat.Json;

        private string CustomSerialize(object @object)
        {
            JObject jsonObject;

            if (@object is MarginProfile marginProfile)
            {
                var jsonSerializer = new JsonSerializer();
                var serializeString = jsonSerializer.Serialize(@object);
                jsonObject = JObject.Parse(serializeString);
                jsonObject.Remove("mileMarkups");

                JObject mileMarkups = new JObject();
                marginProfile.mileMarkups.ForEach(mile =>
                {
                    mileMarkups.Add(new JProperty(mile.MileStart.ToString(), mile.Markup));
                });

                jsonObject.Add("mileMarkups", mileMarkups);

                serializeString = jsonObject.ToString(Newtonsoft.Json.Formatting.None);
                return serializeString;
            }
            throw new System.Exception($"In Compitable Object {@object.ToString()}");
        }
    }

}
