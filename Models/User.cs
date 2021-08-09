﻿using Microsoft.EntityFrameworkCore;
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
        public Membership Membership { get; set; }
        public int? MembershipId { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public User()
        {
            CreatedAt = DateTime.Now;
            Roles = new HashSet<Role>();
        }
    }

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
        public virtual ICollection<LtlDirectory> LtlDirectories { get; set; }
        public UserInfo()
        {
            CreatedAt = DateTime.Now;
            LtlDirectories = new HashSet<LtlDirectory>();
        }
    }

}
