using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class addstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "DoBookItems");

            migrationBuilder.AddColumn<Guid>(
                name: "StatusId",
                table: "DoBookItems",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DoStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoBookItems_StatusId",
                table: "DoBookItems",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoBookItems_DoStatus",
                table: "DoBookItems",
                column: "StatusId",
                principalTable: "DoStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoBookItems_DoStatus",
                table: "DoBookItems");

            migrationBuilder.DropTable(
                name: "DoStatus");

            migrationBuilder.DropIndex(
                name: "IX_DoBookItems_StatusId",
                table: "DoBookItems");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "DoBookItems");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "DoBookItems",
                nullable: true);
        }
    }
}
