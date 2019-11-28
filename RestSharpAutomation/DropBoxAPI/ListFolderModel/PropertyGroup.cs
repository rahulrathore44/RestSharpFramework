using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.DropBoxAPI.ListFolderModel
{
    public class PropertyGroup
    {
        public string template_id { get; set; }
        public List<Field> fields { get; set; }
    }
}
