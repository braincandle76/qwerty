using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QwertyAPI.Models;

namespace QwertyAPI.Tests.Utils
{
    public static class TestData
    {
        public static readonly string PROFILE_NAME = "Bilbo Baggins";

        public static readonly string FAVORITE_COLOR = "Gold";

        public static async Task SeedDBWithData(QwertyDbContext db)
        {
            AddProfiles(db);
            AddQwertyFavColor(db);

            await db.SaveChangesAsync();
        }

        private static void AddProfiles(QwertyDbContext db)
        {
            var colors = db.QwertyFavColors.Local.First();
            var profile = new QwertyProfile
            {
                Name = PROFILE_NAME,
                FavColor = colors
            };

            db.QwertyProfiles.Add(profile);
            db.QwertyProfiles.Add(new QwertyProfile { });
        }

        private static void AddQwertyFavColor(QwertyDbContext db)
        {
            db.QwertyFavColors.Add(new QwertyFavColor { Color = "Purple" });
            db.QwertyFavColors.Add(new QwertyFavColor { Color = "Pink" });
            db.QwertyFavColors.Add(new QwertyFavColor { Color = "Cerulean" });
        }
    }
}