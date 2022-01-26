using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QwertyApi.Migrations
{
    public partial class InsertQwertyMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("QwertyMessages", new string[] { "Text", "QwertyProfileId" }, new string[] { "Hello Database", "1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("QwertyMessages", new string[] { "Text", "QwertyProfileId" }, new string[] { "Hello Database", "1" });
        }
    }
}
