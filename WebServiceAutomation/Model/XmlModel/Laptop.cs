using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebServiceAutomation.Model.XmlModel
{
    [XmlRoot(ElementName = "Laptop")]
    public class Laptop
    {
        [XmlElement(ElementName = "BrandName")]
        public string BrandName { get; set; }
        [XmlElement(ElementName = "Features")]
        public Features Features { get; set; }
        [XmlElement(ElementName = "Id")]
        public string Id { get; set; }
        [XmlElement(ElementName = "LaptopName")]
        public string LaptopName { get; set; }
    }
}
