using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;


namespace qwertyApi.Models
{
    public class qwertyContext : DbContext
    {
        public qwertyContext(DbContextOptions<qwertyContext> options)
            : base(options)
        {
        }

        public DbSet<qwertyItem> qwertyItems { get; set; } = null!;
    }
}