using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class updateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropIndex(
                name: "IX_DoBooks_LanguageId",
                table: "DoBooks");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "DoBooks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LanguageId",
                table: "DoBooks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoBooks_LanguageId",
                table: "DoBooks",
                column: "LanguageId");


        }
    }
}
