using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.Question.Model
{
    public class MarginProfile
    {
        public int customerId { get; set; }
        public string trailerGroup { get; set; }
        public int fuelSurcharge { get; set; }
        public bool overrideDefaultFuelSurcharge { get; set; }
        public bool overrideDefaultLDIMarkup { get; set; }
        public int truckPayAdjustment { get; set; }
        public bool overrideDefaultTruckPayAdjustment { get; set; }
        public bool overrideDefaultMileMarkups { get; set; }
        public List<MileMarkups> mileMarkups { get; set; }

        public LdiMarkups ldiMarkups { get; set; }
        public List<AccessorialMarkup> accessorialMarkups { get; set; }
    }
}
