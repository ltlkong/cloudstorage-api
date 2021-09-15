using Microsoft.EntityFrameworkCore;
using System;

namespace ltl_cloudstorage.Models
{
    public class CSDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Membership> Memberships { get; set; }
        public virtual DbSet<LtlDirectory> LtlDirectories { get; set; }
        public virtual DbSet<LtlFile> LtlFiles { get; set; }
        public CSDbContext(DbContextOptions<CSDbContext> options) : base(options)
        {

        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseMySQL("server=localhost;database=WebDevDb;user=root;password=");
        // }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            this.SeedMemberships(builder);
            this.SeedPublicInfo(builder);
            this.SeedUsers(builder);
        }

        private void SeedMemberships(ModelBuilder builder)
        {
            Membership[] memberships = new Membership[]
            {
                new Membership()
                {
                    Id=1,
                    Name = "Bronze",
                    Color = "linear-gradient(90deg, rgb(195, 88, 43) 0%, rgb(255, 182, 117) 50%, rgb(195, 88, 43) 100%)",
                    Description = "# Bronze",
                    Price = 10
                },

                new Membership()
                {
                    Id=2,
                    Name = "Silver",
                    Color = "linear-gradient(90deg, rgba(151,150,149,1) 0%, rgba(210,210,210,1) 50%, rgba(151,150,149,1) 100%)",
                    Description = "",
                    Price = 15
                },

                new Membership()
                {
                    Id=3,
                    Name = "Gold",
                    Color = "linear-gradient(90deg, rgba(249,194,56,1) 0%, rgba(255,236,188,1) 50%, rgba(249,194,56,1) 100%)",
                    Description = "",
                    Price = 20
                }
            };

            builder.Entity<Membership>().HasData(memberships);
        }

        private void SeedUsers(ModelBuilder builder)
        {
            User[] users = new User[]
            {
                new User()
                {
                    Id=2,
                    Name = "ltl",
                    DisplayName = "ltl",
                    Email = "tielinli@yahoo.com",
                    PasswordHash = "oIn5JKeGBFsnpRAekK4jTQ==",
                    CreatedAt = DateTime.Parse("2021-07-26 23:00:48"),
                    LastLoginAt = DateTime.Parse("2021-07-27 08:19:04"),
                },

                new User()
                {
                    Id=3,
                    Name = "LisaLee",
                    DisplayName = "LisaLee",
                    Email = "1248988727@qq.com",
                    PasswordHash = "eMoP6zKEDM9eDMEYtFm4VA==",
                    CreatedAt = DateTime.Parse("2021-07-27 07:11:41"),
                    LastLoginAt = DateTime.Parse("2021-07-27 07:11:41"),
                }
            };

            builder.Entity<User>().HasData(users);
        }

        private void SeedPublicInfo(ModelBuilder builder) 
        {
			User publicUser = new User()
			{
				Id=1,
				Name = "public",
				DisplayName = "public",
				Email = "public@public.com",
				PasswordHash = "public",
				CreatedAt = DateTime.Now,
				LastLoginAt = DateTime.Now,
			};

            Profile publicInfo = new Profile() 
            {
                Id=1,
                Reputation = 100,
                Introduction = "# Public test user",
                UpdatedAt = DateTime.Now
            };

			LtlDirectory publicDir = new LtlDirectory("13c44ee6-0628-4cfc-9690-5c6bfb357df6", "Default", 1, null);
			publicDir.Id = 1;

			builder.Entity<User>().HasData(publicUser);
            builder.Entity<Profile>().HasData(publicInfo);
			builder.Entity<LtlDirectory>().HasData(publicDir);
        }
    }
}
