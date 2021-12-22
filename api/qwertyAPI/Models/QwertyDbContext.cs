using Microsoft.EntityFrameworkCore;

namespace QwertyAPI.Models
{
    public partial class QwertyDbContext : DbContext
    {
        public QwertyDbContext() { }
        public QwertyDbContext(DbContextOptions<QwertyDbContext> options) : base(options) { }

        public virtual DbSet<QwertyProfile> QwertyProfiles { get; set; }

        //   protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //      modelBuilder.Entity
        //  }
    }
}