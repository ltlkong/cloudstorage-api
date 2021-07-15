using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ltl_pf.Models
{
    [Table("User")]
    [Index(nameof(Name), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string DisplayName { get; set; }
        [Required]
        [MaxLength(300)]
        public string Email { get; set; }
        [JsonIgnore]
        [Required]
        public string PasswordHash { get; set; }
        public byte[] Avatar { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public ICollection<Role> Roles { get; set; }
        public User()
        {
            Roles = new HashSet<Role>();
        }
    }
    [Table("UserInfo")]
    public class UserInfo
    {
        [JsonIgnore]
        [ForeignKey("User")]
        public int Id { get; set; }     
        [Required]
        [Range(0, 100)]
        public int Reputation { get; set; }
        public string Introduction { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }

}
