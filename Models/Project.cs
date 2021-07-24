using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ltl_pf.Models
{
    [Table("Project")]
    public class Project
    {
        public int Id { get; set; }
        [MaxLength(150)]
        public string Name { get; set; }
        [MaxLength(3000)]
        public string Description { get; set; }
        public double Budget { get; set; }
        public int Duration { get; set; }
        public  bool IsInProgress { get; set; }
        public ICollection<UserInfo> Users { get; set; }
    }
}
