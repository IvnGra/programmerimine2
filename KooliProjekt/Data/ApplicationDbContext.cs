using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasNoKey();
            modelBuilder.Entity<leaderboard>().HasNoKey();
            modelBuilder.Entity<match>().HasNoKey();
        }
        public DbSet<tournament> Tournament { get; set; }
        public DbSet<team> Team { get; set; }
        public DbSet<prediction> Prediction { get; set; }
        public DbSet<match> Match { get; set; }
        public DbSet<leaderboard> Leaderboard { get; set; }
        
    }
}