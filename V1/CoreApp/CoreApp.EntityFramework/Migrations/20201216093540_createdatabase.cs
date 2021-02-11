using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class createdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoCatalogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoCatalogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoPublishiers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoPublishiers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoRacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    LocationIndentifier = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoRacks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoBooks_CatalogId",
                table: "DoBooks",
                column: "CatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_DoBooks_PublishierId",
                table: "DoBooks",
                column: "PublishierId");

            migrationBuilder.CreateIndex(
                name: "IX_DoBookItems_RackId",
                table: "DoBookItems",
                column: "RackId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoBookItems_DoRacks",
                table: "DoBookItems",
                column: "RackId",
                principalTable: "DoRacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoBooks_DoCatalogs",
                table: "DoBooks",
                column: "CatalogId",
                principalTable: "DoCatalogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoBooks_DoPublishiers",
                table: "DoBooks",
                column: "PublishierId",
                principalTable: "DoPublishiers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoBookItems_DoRacks",
                table: "DoBookItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DoBooks_DoCatalogs",
                table: "DoBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_DoBooks_DoPublishiers",
                table: "DoBooks");

            migrationBuilder.DropTable(
                name: "DoCatalogs");

            migrationBuilder.DropTable(
                name: "DoPublishiers");

            migrationBuilder.DropTable(
                name: "DoRacks");

            migrationBuilder.DropIndex(
                name: "IX_DoBooks_CatalogId",
                table: "DoBooks");

            migrationBuilder.DropIndex(
                name: "IX_DoBooks_PublishierId",
                table: "DoBooks");

            migrationBuilder.DropIndex(
                name: "IX_DoBookItems_RackId",
                table: "DoBookItems");
        }
    }
}
