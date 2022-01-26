using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QwertyApi.Migrations
{
    public partial class InsertQwertyProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("QwertyProfiles", "Name", "Keyleth Nightbreeze");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("QwertyProfiles", "Name", "Keyleth Nightbreeze");
        }
    }
}
