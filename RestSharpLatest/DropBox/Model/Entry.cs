using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.DropBox.Model
{
    public class Entry
    {
        [JsonProperty(".tag")]
        public string Tag { get; set; }
        public DateTime client_modified { get; set; }
        public string content_hash { get; set; }
        public FileLockInfo file_lock_info { get; set; }
        public bool has_explicit_shared_members { get; set; }
        public string id { get; set; }
        public bool is_downloadable { get; set; }
        public string name { get; set; }
        public string path_display { get; set; }
        public string path_lower { get; set; }
        public List<PropertyGroup> property_groups { get; set; }
        public string rev { get; set; }
        public DateTime server_modified { get; set; }
        public SharingInfo sharing_info { get; set; }
        public int size { get; set; }
    }
}
