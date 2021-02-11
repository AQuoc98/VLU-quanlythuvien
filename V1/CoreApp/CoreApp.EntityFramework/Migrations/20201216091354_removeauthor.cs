using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class removeauthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoBooks_DoAuthors",
                table: "DoBooks");

            migrationBuilder.DropTable(
                name: "DoAuthors");

            migrationBuilder.DropIndex(
                name: "IX_DoBooks_AuthorId",
                table: "DoBooks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoAuthors",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Name = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoAuthors", x => x.ID);
                });

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
    }
}
