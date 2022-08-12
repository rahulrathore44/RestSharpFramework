using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.APIModel.JsonApiModel
{
    public class JsonModelBuilder
    {
        private string _BrandName { get; set; }
        private Features _Features { get; set; }
        private int _Id { get; set; }
        private string _LaptopName { get; set; }

        public JsonModel Build()
        {
            return new JsonModel()
            {
                BrandName = _BrandName,
                Features = _Features,
                Id = _Id,
                LaptopName = _LaptopName
            };
        }

        public JsonModelBuilder WithBrandName(string name)
        {
            _BrandName = name;
            return this;
        }

        public JsonModelBuilder WithId(int id)
        {
            _Id = id;
            return this;
        }

        public JsonModelBuilder WithLaptopName(string name)
        {
            _LaptopName = name;
            return this;
        }

        public JsonModelBuilder WithFeatures(List<string> feature)
        {
            _Features = new Features()
            {
                Feature = feature
            };
            return this;
        }
    }
}
