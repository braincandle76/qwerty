using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QwertyApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QwertyProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QwertyProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QwertyMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QwertyProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QwertyMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QwertyMessages_QwertyProfiles_QwertyProfileId",
                        column: x => x.QwertyProfileId,
                        principalTable: "QwertyProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QwertyMessages_QwertyProfileId",
                table: "QwertyMessages",
                column: "QwertyProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_QwertyProfiles_Id",
                table: "QwertyProfiles",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QwertyMessages");

            migrationBuilder.DropTable(
                name: "QwertyProfiles");
        }
    }
}
