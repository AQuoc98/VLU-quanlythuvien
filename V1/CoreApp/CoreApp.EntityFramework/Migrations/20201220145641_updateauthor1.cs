using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class updateauthor1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "DoAuthors");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "DoAuthors");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "DoAuthors");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "DoAuthors");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "DoAuthors",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "DoAuthors",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "DoAuthors",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "DoAuthors",
                type: "datetime",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DoAuthors");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "DoAuthors");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DoAuthors");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "DoAuthors");

            migrationBuilder.AddColumn<Guid>(
                name: "CreateBy",
                table: "DoAuthors",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "DoAuthors",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateBy",
                table: "DoAuthors",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "DoAuthors",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
