using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ltl_cloudstorage.Models
{
    [Index(nameof(UniqueId), IsUnique = true)]
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
			isDeleted = false;
			CreatedAt = DateTime.Now;
        }

        public int Id { get; set; }
        public string UniqueId { get; set; }
        [MaxLength(300)]
        public string Name { get; set; }
        // Context is where the Storage folder located
		[JsonIgnore]
        public string Path { get; set; }
        [MaxLength(40)]
        public string Type { get; set; }
		[JsonIgnore]
		public bool isDeleted { get; set;}
        public long Size { get; set; }
		[Required]
		public DateTime CreatedAt { get; set; }
        [ForeignKey("Directory")]
        public int DirectoryId { get; set; }
        [JsonIgnore]
        public virtual LtlDirectory Directory { get; set; }
    }
}
