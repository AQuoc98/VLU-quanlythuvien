using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class updatelanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "DoBooks");

            migrationBuilder.AddColumn<Guid>(
                name: "LanguageId",
                table: "DoBooks",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoBooks_LanguageId",
                table: "DoBooks",
                column: "LanguageId");

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropIndex(
                name: "IX_DoBooks_LanguageId",
                table: "DoBooks");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "DoBooks");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "DoBooks",
                maxLength: 500,
                nullable: true);
        }
    }
}
