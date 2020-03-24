using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.Question.Model
{
    public class MileMarkups
    {
        public MileMarkups(int mileStart, decimal markup)
        {
            MileStart = mileStart;
            Markup = markup;
        }
        public int MileStart { get; set; }
        public decimal Markup { get; set; }
    }
}
