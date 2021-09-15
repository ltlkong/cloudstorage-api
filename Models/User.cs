using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ltl_cloudstorage.Models
{
    [Index(nameof(Name), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        public string DisplayName { get; set; }
        [Required]
        [MaxLength(300)]
        public string Email { get; set; }
        [JsonIgnore]
        [Required]
        public string PasswordHash { get; set; }
        [MaxLength(500)]
        public string Avatar { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime LastLoginAt { get; set; }
        public virtual ICollection<Membership> Memberships { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public User()
        {
            CreatedAt = DateTime.Now;
            Roles = new HashSet<Role>();
			Memberships = new HashSet<Membership>();
        }
    }

    public class Profile
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
        public virtual User User { get; set; }
        public Profile()
        {
            CreatedAt = DateTime.Now;
			UpdatedAt = DateTime.Now;
        }
    }

}
