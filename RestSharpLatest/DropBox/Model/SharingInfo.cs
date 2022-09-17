using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.DropBox.Model
{
    public class SharingInfo
    {
        public string modified_by { get; set; }
        public string parent_shared_folder_id { get; set; }
        public bool read_only { get; set; }
        public bool? no_access { get; set; }
        public bool? traverse_only { get; set; }
    }
}
