using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class fk_book_author : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "DoBooks",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_DoBooks_AuthorId",
                table: "DoBooks",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoBooks_DoAuthors",
                table: "DoBooks",
                column: "AuthorId",
                principalTable: "DoAuthors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoBooks_DoAuthors",
                table: "DoBooks");

            migrationBuilder.DropIndex(
                name: "IX_DoBooks_AuthorId",
                table: "DoBooks");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "DoBooks");


        }
    }
}
