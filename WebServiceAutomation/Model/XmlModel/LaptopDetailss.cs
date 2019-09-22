using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebServiceAutomation.Model.XmlModel
{
    [XmlRoot(ElementName = "laptopDetailss")]
    public class LaptopDetailss
    {
        [XmlElement(ElementName = "Laptop")]
        public Laptop Laptop { get; set; }
    }
}
