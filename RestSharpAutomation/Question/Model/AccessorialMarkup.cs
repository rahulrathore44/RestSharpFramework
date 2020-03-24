using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.Question.Model
{
    public class AccessorialMarkup
    {
        public string accessorialCode { get; set; }
        public int markup { get; set; }
        public bool overrideDefault { get; set; }
        public bool notRated { get; set; }
    }

}
