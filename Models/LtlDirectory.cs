using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ltl_cloudstorage.Models
{
    public class LtlDirectory
    {
        public LtlDirectory(string uniqueId, string name)
        {
            UniqueId = uniqueId;
            Name = name;
            CreatedAt = DateTime.Now;
            Documents = new HashSet<LtlFile>();
        }
        public int Id { get; set; }
        public string UniqueId { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        [ForeignKey("UserInfo")]
        public int UserInfoId { get; set; }
        public UserInfo UserInfo { get; set; }
        public virtual ICollection<LtlFile> Documents { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }
}
