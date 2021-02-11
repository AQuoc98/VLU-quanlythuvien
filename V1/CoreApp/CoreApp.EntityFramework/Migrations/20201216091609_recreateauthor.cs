using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class recreateauthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoAuthor",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoAuthor", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoBooks_AuthorId",
                table: "DoBooks",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoBooks_DoAuthors",
                table: "DoBooks",
                column: "AuthorId",
                principalTable: "DoAuthor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoBooks_DoAuthors",
                table: "DoBooks");

            migrationBuilder.DropTable(
                name: "DoAuthor");

            migrationBuilder.DropIndex(
                name: "IX_DoBooks_AuthorId",
                table: "DoBooks");
        }
    }
}
