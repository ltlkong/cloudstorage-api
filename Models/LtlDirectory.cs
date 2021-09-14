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
    public class LtlDirectory
    {
        public LtlDirectory(string uniqueId, string name, int userInfoId, int? parentDirId)
        {
            UniqueId = uniqueId;
            Name = name;
            UserInfoId = userInfoId;
			ParentDirId = parentDirId;
            CreatedAt = DateTime.Now;
            Files = new HashSet<LtlFile>();
			ChildDirs = new HashSet<LtlDirectory>();
        }
        public int Id { get; set; }
        public string UniqueId { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        [ForeignKey("UserInfo")]
        public int UserInfoId { get; set; }
		[JsonIgnore]
        public UserInfo UserInfo { get; set; }
		[ForeignKey("LtlDirectory")]
		public int? ParentDirId { get; set; }
		[JsonIgnore]
		public LtlDirectory ParentDir { get; set; }
		[JsonIgnore]
        public virtual ICollection<LtlFile> Files { get; set; }
		[JsonIgnore]
		public virtual ICollection<LtlDirectory> ChildDirs { get; set; }
        
    }
}
