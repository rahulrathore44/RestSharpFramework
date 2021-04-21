using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.CustomSerializer
{
    public class CustomXmlSerializer : IRestSerializer
    {
        string[] IRestSerializer.SupportedContentTypes => throw new NotImplementedException();

        DataFormat IRestSerializer.DataFormat => throw new NotImplementedException();

        string ISerializer.ContentType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        T IDeserializer.Deserialize<T>(IRestResponse response)
        {
            throw new NotImplementedException();
        }

        string IRestSerializer.Serialize(Parameter parameter)
        {
            throw new NotImplementedException();
        }

        string ISerializer.Serialize(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
