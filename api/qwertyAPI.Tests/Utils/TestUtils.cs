using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QwertyAPI.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace QwertyAPI.Tests.Utils
{
    public static class TestUtils
    {
        public static readonly string PROFILE_NAME = "Fargrim Fireforge";

        public static async Task<QwertyDbContext> GetTestDbContext()
        {
            var db = new QwertyDbContext(CreateOptions());
            await db.Database.EnsureDeletedAsync();
            await db.Database.EnsureCreatedAsync();

            var profile = new QwertyProfile(PROFILE_NAME);

            await db.SaveChangesAsync();

            return db;
        }

        private static DbContextOptions<QwertyDbContext> CreateOptions()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            var builder = new DbContextOptionsBuilder<QwertyDbContext>();
            builder.UseSqlite(connection);

            builder.ConfigureWarnings(x => x.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.AmbientTransactionWarning));

            return builder.Options;
        }
    }
}