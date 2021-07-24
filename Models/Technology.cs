using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ltl_pf.Models
{
    [Table("Technology")]
    [Index(nameof(Name), IsUnique = true)]
    public class Technology
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public ICollection<UserKnowTechnology> UserKnowTechnologies { get; set; }

        public Technology()
        {
            UserKnowTechnologies = new HashSet<UserKnowTechnology>();
        }
    }
    [Table("UserKnowTechnology")]
    public class UserKnowTechnology
    {
        public int Id { get; set; }

        public UserInfo UserInfo { get; set; }
        public Technology Technology { get; set; }
        // User proficiency 1 - 5
        [Range(1,5)]
        public int Level { get; set; }

    }
}
