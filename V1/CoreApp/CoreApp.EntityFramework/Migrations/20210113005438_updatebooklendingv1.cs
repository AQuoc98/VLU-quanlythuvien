using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class updatebooklendingv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<Guid>(
                name: "MemberId",
                table: "DoBookLendings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoBookLendings_MemberId",
                table: "DoBookLendings",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoBookLendings_Members",
                table: "DoBookLendings",
                column: "MemberId",
                principalTable: "DoMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoBookLendings_Members",
                table: "DoBookLendings");

            migrationBuilder.DropIndex(
                name: "IX_DoBookLendings_MemberId",
                table: "DoBookLendings");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "DoBookLendings");

           
        }
    }
}
