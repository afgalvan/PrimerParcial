using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Lodging> Lodgings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FellowLodging>();
            modelBuilder.Entity<GeneralLodging>();
            modelBuilder.Entity<PremiumLodging>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
