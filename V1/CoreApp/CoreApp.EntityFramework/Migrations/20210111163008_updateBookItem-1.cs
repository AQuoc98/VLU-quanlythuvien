using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class updateBookItem1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "DoBookItems",
                nullable: false,
                oldClrType: typeof(short));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AlterColumn<short>(
                name: "Status",
                table: "DoBookItems",
                nullable: false,
                oldClrType: typeof(bool));
        }
    }
}
