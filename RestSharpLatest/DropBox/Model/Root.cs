using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.DropBox.Model
{
    public class Root
    {
        public string cursor { get; set; }
        public List<Entry> entries { get; set; }
        public bool has_more { get; set; }
    }

}
