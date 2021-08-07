using Microsoft.EntityFrameworkCore;

namespace ltl_cloudstorage.Models
{
    public class CSDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserInfo> UserInfos { get; set; }
        public virtual DbSet<Membership> Memberships { get; set; }
        public virtual DbSet<Directory> Directories { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public CSDbContext(DbContextOptions<CSDbContext> options) : base(options)
        {

        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseMySQL("server=localhost;database=WebDevDb;user=root;password=");
        // }
        
    }
}
