using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class updatefklan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
               name: "FK_DoBooks_DoLanguages",
               table: "DoBooks",
               column: "LanguageId",
               principalTable: "DoLanguages",
               principalColumn: "Id",
               onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_DoBooks_DoLanguages",
                table: "DoBooks");
        }
    }
}
