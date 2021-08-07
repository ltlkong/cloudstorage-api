using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ltl_cloudstorage.Models
{
    public class Directory
    {
        public int Id { get; set; }
        public string UniqueId { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public Directory()
        {
            Documents = new HashSet<Document>();
        }
    }
}
