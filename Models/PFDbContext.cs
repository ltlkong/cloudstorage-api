using Microsoft.EntityFrameworkCore;

namespace ltl_cloudstorage.Models
{
    public class PFDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public PFDbContext(DbContextOptions<PFDbContext> options) : base(options)
        {

        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseMySQL("server=localhost;database=WebDevDb;user=root;password=");
        // }
        
    }
}
