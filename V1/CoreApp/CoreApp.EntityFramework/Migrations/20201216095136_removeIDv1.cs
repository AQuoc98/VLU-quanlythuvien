using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class removeIDv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoBookItems_DoFormats",
                table: "DoBookItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DoMembers_DoMemberGroups",
                table: "DoMembers");

            migrationBuilder.DropTable(
                name: "DoFormats");

            migrationBuilder.DropTable(
                name: "DoMemberGroups");

            migrationBuilder.DropIndex(
                name: "IX_DoMembers_MemberGroupId",
                table: "DoMembers");

            migrationBuilder.DropIndex(
                name: "IX_DoBookItems_FormatId",
                table: "DoBookItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoFormats",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Name = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoFormats", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DoMemberGroups",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Name = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoMemberGroups", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoMembers_MemberGroupId",
                table: "DoMembers",
                column: "MemberGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_DoBookItems_FormatId",
                table: "DoBookItems",
                column: "FormatId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoBookItems_DoFormats",
                table: "DoBookItems",
                column: "FormatId",
                principalTable: "DoFormats",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoMembers_DoMemberGroups",
                table: "DoMembers",
                column: "MemberGroupId",
                principalTable: "DoMemberGroups",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
