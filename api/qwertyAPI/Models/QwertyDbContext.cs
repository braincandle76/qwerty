using Microsoft.EntityFrameworkCore;

namespace QwertyAPI.Models
{
    public partial class QwertyDbContext : DbContext
    {
        public QwertyDbContext() { }
        public QwertyDbContext(DbContextOptions<QwertyDbContext> options) : base(options) { }

        public virtual DbSet<QwertyProfile> QwertyProfiles { get; set; }
        public virtual DbSet<QwertyMessage> QwertyMessages { get; set; }
        public virtual DbSet<QwertyFavColor> QwertyFavColors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QwertyProfile>()
            .HasIndex(e => e.Id);

            modelBuilder.Entity<QwertyMessage>()
            .HasIndex(e => e.Id);

            modelBuilder.Entity<QwertyFavColor>()
            .HasIndex(e => e.Id);
        }
    }
}