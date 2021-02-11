using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class addfklabo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LanguageId",
                table: "DoBooks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoBooks_LanguageId",
                table: "DoBooks",
                column: "LanguageId");

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

            migrationBuilder.DropIndex(
                name: "IX_DoBooks_LanguageId",
                table: "DoBooks");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "DoBooks");
        }
    }
}
