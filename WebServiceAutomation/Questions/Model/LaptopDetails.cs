using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceAutomation.Questions.Model
{
    [Serializable]
    public class LaptopDetails
    {
        public string BrandName { get; set; }

        public LapTopFeatures Features { get; set; }

        public int Id { get; set; }

        public string LaptopName { get; set; }
    }
}
