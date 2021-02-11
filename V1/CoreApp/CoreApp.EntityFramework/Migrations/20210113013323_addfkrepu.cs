using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class addfkrepu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MemberId",
                table: "DoReservationBooks",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MemberId",
                table: "DoPunishments",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Gender",
                table: "DoMembers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "DoMembers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoReservationBooks_MemberId",
                table: "DoReservationBooks",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_DoPunishments_MemberId",
                table: "DoPunishments",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoPunishments_DoMembers",
                table: "DoPunishments",
                column: "MemberId",
                principalTable: "DoMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoReservationBooks_DoMembers",
                table: "DoReservationBooks",
                column: "MemberId",
                principalTable: "DoMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoPunishments_DoMembers",
                table: "DoPunishments");

            migrationBuilder.DropForeignKey(
                name: "FK_DoReservationBooks_DoMembers",
                table: "DoReservationBooks");

            migrationBuilder.DropIndex(
                name: "IX_DoReservationBooks_MemberId",
                table: "DoReservationBooks");

            migrationBuilder.DropIndex(
                name: "IX_DoPunishments_MemberId",
                table: "DoPunishments");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "DoReservationBooks");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "DoPunishments");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "DoMembers");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "DoMembers");
        }
    }
}
