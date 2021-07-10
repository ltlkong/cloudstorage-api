﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ltl_webdev.Models
{
    public class WebDevDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public WebDevDbContext(DbContextOptions<WebDevDbContext> options) : base(options)
        {

        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseMySQL("server=localhost;database=WebDevDb;user=root;password=");
        // }
        
    }
}
