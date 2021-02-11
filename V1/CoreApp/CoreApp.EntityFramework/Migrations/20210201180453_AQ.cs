using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class AQ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoReservationBooks_DoBookItems_TestId",
                table: "DoReservationBooks");

            migrationBuilder.DropIndex(
                name: "IX_DoReservationBooks_TestId",
                table: "DoReservationBooks");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "DoReservationBooks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TestId",
                table: "DoReservationBooks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoReservationBooks_TestId",
                table: "DoReservationBooks",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoReservationBooks_DoBookItems_TestId",
                table: "DoReservationBooks",
                column: "TestId",
                principalTable: "DoBookItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
