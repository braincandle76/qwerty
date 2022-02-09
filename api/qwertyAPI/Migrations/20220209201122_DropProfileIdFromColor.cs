using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QwertyApi.Migrations
{
    public partial class DropProfileIdFromColor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QwertyFavColors_QwertyProfiles_QwertyProfileId",
                table: "QwertyFavColors");

            migrationBuilder.DropIndex(
                name: "IX_QwertyFavColors_QwertyProfileId",
                table: "QwertyFavColors");

            migrationBuilder.DropColumn(
                name: "QwertyProfileId",
                table: "QwertyFavColors");

            migrationBuilder.AddColumn<int>(
                name: "QwertyFavColorId",
                table: "QwertyProfiles",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_QwertyProfiles_QwertyFavColorId",
                table: "QwertyProfiles",
                column: "QwertyFavColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_QwertyProfiles_QwertyFavColors_QwertyFavColorId",
                table: "QwertyProfiles",
                column: "QwertyFavColorId",
                principalTable: "QwertyFavColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QwertyProfiles_QwertyFavColors_QwertyFavColorId",
                table: "QwertyProfiles");

            migrationBuilder.DropIndex(
                name: "IX_QwertyProfiles_QwertyFavColorId",
                table: "QwertyProfiles");

            migrationBuilder.DropColumn(
                name: "QwertyFavColorId",
                table: "QwertyProfiles");

            migrationBuilder.AddColumn<int>(
                name: "QwertyProfileId",
                table: "QwertyFavColors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_QwertyFavColors_QwertyProfileId",
                table: "QwertyFavColors",
                column: "QwertyProfileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QwertyFavColors_QwertyProfiles_QwertyProfileId",
                table: "QwertyFavColors",
                column: "QwertyProfileId",
                principalTable: "QwertyProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
