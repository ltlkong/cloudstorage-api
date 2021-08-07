using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ltl_cloudstorage.Models
{
    public class LtlFile
    {
        public LtlFile(string uniqueId, string name,string type, string path, long size, int directoryId)
        {
            UniqueId = uniqueId;
            Type = type;
            Name = name;
            Path = path;
            Size = size;
            DirectoryId = directoryId;
        }

        public int Id { get; set; }
        public string UniqueId { get; set; }
        [MaxLength(300)]
        public string Name { get; set; }
        // Context is where the Storage folder located
        public string Path { get; set; }
        [MaxLength(40)]
        public string Type { get; set; }
        public long Size { get; set; }
        [ForeignKey("Directory")]
        public int DirectoryId { get; set; }
        [JsonIgnore]
        public LtlDirectory Directory { get; set; }
    }
}
