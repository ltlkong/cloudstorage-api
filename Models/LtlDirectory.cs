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
        public LtlDirectory(string uniqueId, string name, int userInfoId)
        {
            UniqueId = uniqueId;
            Name = name;
            UserInfoId = userInfoId;
            CreatedAt = DateTime.Now;
            Documents = new HashSet<LtlFile>();
        }
        public int Id { get; set; }
        public string UniqueId { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        [ForeignKey("UserInfo")]
        public int UserInfoId { get; set; }
        [JsonIgnore]
        public UserInfo UserInfo { get; set; }
        public virtual ICollection<LtlFile> Documents { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }
}
