using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class updatebookitem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfPurchase",
                table: "DoBookItems");

            migrationBuilder.DropColumn(
                name: "Format",
                table: "DoBookItems");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "DoBookItems",
                newName: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "DoBookItems",
                newName: "status");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfPurchase",
                table: "DoBookItems",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<short>(
                name: "Format",
                table: "DoBookItems",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
