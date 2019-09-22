using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebServiceAutomation.Helper.Response
{
    public class ResponseDataHelper
    {

        // ResponseDataHelper.DeserializeJsonResponse<JsonRootObject>(restResponse.ResponseContent)

        /*
          public static JsonRootObject DeserializeJsonResponse<JsonRootObject>(string responseData) where JsonRootObject : class
        {
            return JsonConvert.DeserializeObject<JsonRootObject>(responseData);
        }
             
             */

        // ResponseDataHelper.DeserializeJsonResponse<Features>(restResponse.ResponseContent)

        /*
          public static Features DeserializeJsonResponse<Features>(string responseData) where Features : class
        {
            return JsonConvert.DeserializeObject<Features>(responseData);
        }
             
             */

        public static T DeserializeJsonResponse<T>(string responseData) where T : class
        {
            return JsonConvert.DeserializeObject<T>(responseData);
        }


        // ResponseDataHelper.DeserializeXmlResponse<LaptopDetailss>(restResponse.ResponseContent)

        /*

          public static LaptopDetailss DeserializeXmlResponse<LaptopDetailss>(string responseData) where LaptopDetailss : class
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(LaptopDetailss));
        TextReader textReader = new StringReader(responseData);
        return (LaptopDetailss)xmlSerializer.Deserialize(textReader);
    }


         */

        // ResponseDataHelper.DeserializeXmlResponse<Laptop>(restResponse.ResponseContent)

        /*

          public static Laptop DeserializeXmlResponse<Laptop>(string responseData) where Laptop : class
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Laptop));
        TextReader textReader = new StringReader(responseData);
        return (Laptop)xmlSerializer.Deserialize(textReader);
    }


         */


        public static T DeserializeXmlResponse<T>(string responseData) where T : class
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            TextReader textReader = new StringReader(responseData);
            return (T)xmlSerializer.Deserialize(textReader);
        }
    }
}
