using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class fk_bookitem_rack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<Guid>(
                name: "RackId",
                table: "DoBookItems",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_DoBookItems_RackId",
                table: "DoBookItems",
                column: "RackId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoBookItems_DoRacks",
                table: "DoBookItems",
                column: "RackId",
                principalTable: "DoRacks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoBookItems_DoRacks",
                table: "DoBookItems");

            migrationBuilder.DropIndex(
                name: "IX_DoBookItems_RackId",
                table: "DoBookItems");

            migrationBuilder.DropColumn(
                name: "RackId",
                table: "DoBookItems");

          
        }
    }
}
