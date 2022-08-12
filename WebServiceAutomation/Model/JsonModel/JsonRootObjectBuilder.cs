using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceAutomation.Model.JsonModel
{
    public class JsonRootObjectBuilder
    {
        private string _BrandName { get; set; }
        private Features _Features { get; set; }
        private int _Id { get; set; }
        private string _LaptopName { get; set; }

        public JsonRootObject Build()
        {
            return new JsonRootObject()
            {
                BrandName = _BrandName,
                Features = _Features,
                Id = _Id,
                LaptopName = _LaptopName
            };
        }

        public JsonRootObjectBuilder WithBrandName(string name)
        {
            _BrandName = name;
            return this;
        }

        public JsonRootObjectBuilder WithId(int id)
        {
            _Id = id;
            return this;
        }

        public JsonRootObjectBuilder WithLaptopName(string name)
        {
            _LaptopName = name;
            return this;
        }

        public JsonRootObjectBuilder WithFeatures(List<string> feature)
        {
            _Features = new Features()
            {
                Feature = feature
            };
            return this;
        }
    }
}
