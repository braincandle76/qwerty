using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using QwertyAPI.Models;

namespace QwertyAPI.Tests.Utils
{
    public static class TestUtils
    {
        public static readonly string PROFILE_NAME = "Fargrim Fireforge";
        public static readonly string ADDED_NAME = "Keyleth Nightbreeze";

        public static async Task<QwertyDbContext> GetTestDbContext()
        {
            var db = new QwertyDbContext(CreateOptions());
            await db.Database.EnsureDeletedAsync();
            await db.Database.EnsureCreatedAsync();
            await TestData.SeedDBWithData(db);
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