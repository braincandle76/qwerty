using Microsoft.EntityFrameworkCore;

namespace QwertyApi.Models
{
    public class QwertyDbContext : DbContext
    {
        public QwertyDbContext() { }
        public QwertyDbContext(DbContextOptions<QwertyDbContext> options): base(options){ }

        public virtual DbSet<QwertyProfile> QwertyProfiles { get; set; }
    }
}