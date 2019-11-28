using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.JiraAPI.Response
{
    public class CreateProjectResponse
    {
        public string self { get; set; }
        public int id { get; set; }
        public string key { get; set; }
    }
}
