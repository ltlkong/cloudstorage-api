using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ltl_cloudstorage.Models
{
    [Table("Membership")]
    public class Membership
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        [MaxLength(150)]
        public string Color { get; set; }
        [Required]
        [MaxLength(600)]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
