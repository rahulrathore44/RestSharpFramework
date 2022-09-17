using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.DropBox.Model
{
    public class PropertyGroup
    {
        public List<Field> fields { get; set; }
        public string template_id { get; set; }
    }
}
