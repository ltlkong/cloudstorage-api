using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ltl_webdev.Models
{
    [Table("Role")]
    [Index(nameof(Name), IsUnique = true)]
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
        public Role()
        {
            Users = new HashSet<User>();
        }
    }
}
