using KooliProjekt.Search;
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
        public DbSet<User> Users { get; set; }

        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Prediction> Predictions { get; set; }
        public DbSet<Match> Matchs { get; set; }
        public DbSet<Leaderboard> Leaderboards { get; set; }

        public void List(object page, int v)
        {
            throw new NotImplementedException();
        }

        internal async Task<PagedResult<User>> List(int page, int v, UsersSearch search)
        {
            throw new NotImplementedException();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Modify Team1Id foreign key constraint to prevent cascade delete (to avoid cycles)
            modelBuilder.Entity<Match>()
                .HasOne(m => m.Team1_name)
                .WithMany() // No navigation property on Team for Matches
                .HasForeignKey(m => m.Team1Id)
                .OnDelete(DeleteBehavior.NoAction);  // You can also use DeleteBehavior.Restrict

            // Modify Team2Id foreign key constraint to prevent cascade delete (to avoid cycles)
            modelBuilder.Entity<Match>()
                .HasOne(m => m.Team2_name)
                .WithMany() // No navigation property on Team for Matches
                .HasForeignKey(m => m.Team2Id)
                .OnDelete(DeleteBehavior.NoAction);  // You can also use DeleteBehavior.Restrict
        }

    }
}