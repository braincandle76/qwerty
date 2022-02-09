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

            migrationBuilder.Sql(@"
                INSERT INTO dbo.QwertyProfiles (Name) VALUES ('Keith');
                INSERT INTO dbo.QwertyProfiles (Name) VALUES ('Marla');
                INSERT INTO dbo.QwertyProfiles (Name) VALUES ('Dawn');
                INSERT INTO dbo.QwertyProfiles (Name) VALUES ('Theo');
                INSERT INTO dbo.QwertyFavColors (Color, QwertyProfileId) VALUES ('Red', 1);
                INSERT INTO dbo.QwertyFavColors (Color, QwertyProfileId) VALUES ('Orange', 2);
                INSERT INTO dbo.QwertyFavColors (Color, QwertyProfileId) VALUES ('Yellow', 3);
                INSERT INTO dbo.QwertyFavColors (Color, QwertyProfileId) VALUES ('Green', 4);
                INSERT INTO dbo.QwertyFavColors (Color, QwertyProfileId) VALUES ('Blue', 5);
            ");
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
