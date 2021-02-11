using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class updatepuli : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicationDate",
                table: "DoBookItems");

            migrationBuilder.AddColumn<string>(
                name: "PublicationYear",
                table: "DoBookItems",
                maxLength: 4,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicationYear",
                table: "DoBookItems");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublicationDate",
                table: "DoBookItems",
                type: "datetime",
                nullable: true);
        }
    }
}
