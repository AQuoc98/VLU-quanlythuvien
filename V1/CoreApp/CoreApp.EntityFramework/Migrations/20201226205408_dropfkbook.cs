using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class dropfkbook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoBookItems_DoBooks",
                table: "DoBookItems");



            migrationBuilder.DropIndex(
                name: "IX_DoBookItems_BookId",
                table: "DoBookItems");



            migrationBuilder.DropColumn(
                name: "BookId",
                table: "DoBookItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AddColumn<Guid>(
                name: "BookId",
                table: "DoBookItems",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));



            migrationBuilder.CreateIndex(
                name: "IX_DoBookItems_BookId",
                table: "DoBookItems",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoBookItems_DoBooks",
                table: "DoBookItems",
                column: "BookId",
                principalTable: "DoBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);


        }
    }
}
