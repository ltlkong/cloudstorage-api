using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ltl_cloudstorage.Models
{
    public class Membership
    {
        [JsonIgnore]
        public int Id { get; set; }
		[MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Color { get; set; }
        [Required]
        [MaxLength(600)]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
		[JsonIgnore]
		public virtual ICollection<User> Users { get; set; }
		public Membership() 
		{
			Users = new HashSet<User>();
		}
    }
}
