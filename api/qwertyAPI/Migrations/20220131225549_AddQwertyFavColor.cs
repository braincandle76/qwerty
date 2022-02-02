using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QwertyApi.Migrations
{
    public partial class AddQwertyFavColor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QwertyFavColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QwertyProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QwertyFavColors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QwertyFavColors_QwertyProfiles_QwertyProfileId",
                        column: x => x.QwertyProfileId,
                        principalTable: "QwertyProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QwertyMessages_Id",
                table: "QwertyMessages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_QwertyFavColors_Id",
                table: "QwertyFavColors",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_QwertyFavColors_QwertyProfileId",
                table: "QwertyFavColors",
                column: "QwertyProfileId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QwertyFavColors");

            migrationBuilder.DropIndex(
                name: "IX_QwertyMessages_Id",
                table: "QwertyMessages");
        }
    }
}
