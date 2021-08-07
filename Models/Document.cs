using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ltl_cloudstorage.Models
{
    public class Document
    {
        public Document(string uniqueId, string name,string type, string path, int directoryId=1)
        {
            UniqueId = uniqueId;
            Type = type;
            Name = name;
            Path = path;
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
        [ForeignKey("Directory")]
        public int DirectoryId { get; set; }
        public Directory Directory { get; set; }
    }
}
