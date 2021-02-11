using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class updatelan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoBooks_DoLanguages",
                table: "DoBooks");

            migrationBuilder.AddForeignKey(
                name: "FK_DoBooks_DoLanguages",
                table: "DoBooks",
                column: "LanguageId",
                principalTable: "DoLanguages",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoBooks_DoLanguages",
                table: "DoBooks");

            migrationBuilder.AddForeignKey(
                name: "FK_DoBooks_DoLanguages",
                table: "DoBooks",
                column: "LanguageId",
                principalTable: "DoLanguages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
