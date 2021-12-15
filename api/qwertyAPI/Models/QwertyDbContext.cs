using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;


namespace QwertyApi.Models
{
    public class QwertyDbContext : DbContext
    {
        public QwertyDbContext(DbContextOptions<QwertyDbContext> options)
            : base(options)
        {
        }

        public DbSet<QwertyItem> QwertyItems { get; set; }
    }
}