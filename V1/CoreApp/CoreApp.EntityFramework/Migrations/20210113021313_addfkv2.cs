using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class addfkv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoBookLendings_BookItems",
                table: "DoBookLendings");

            migrationBuilder.DropForeignKey(
                name: "FK_DoBookLendings_Members",
                table: "DoBookLendings");

            migrationBuilder.DropForeignKey(
                name: "FK_DoMembers_DoMemberGroups",
                table: "DoMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_DoPunishments_DoMembers",
                table: "DoPunishments");

            migrationBuilder.DropForeignKey(
                name: "FK_DoReservationBooks_DoBooks",
                table: "DoReservationBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_DoReservationBooks_DoMembers",
                table: "DoReservationBooks");

            migrationBuilder.AddColumn<Guid>(
                name: "BookItemId",
                table: "DoReservationBooks",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MemberGroupId",
                table: "DoPolicys",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoReservationBooks_BookItemId",
                table: "DoReservationBooks",
                column: "BookItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DoPolicys_MemberGroupId",
                table: "DoPolicys",
                column: "MemberGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoBookLendings_BookItems",
                table: "DoBookLendings",
                column: "BookItemId",
                principalTable: "DoBookItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DoBookLendings_Members",
                table: "DoBookLendings",
                column: "MemberId",
                principalTable: "DoMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DoMembers_DoMemberGroups",
                table: "DoMembers",
                column: "MemberGroupId",
                principalTable: "DoMemberGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DoPolicys_DoMembersGroup",
                table: "DoPolicys",
                column: "MemberGroupId",
                principalTable: "DoMemberGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DoPunishments_DoMembers",
                table: "DoPunishments",
                column: "MemberId",
                principalTable: "DoMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DoReservationBooks_DoBooks",
                table: "DoReservationBooks",
                column: "BookId",
                principalTable: "DoBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DoReservationBooks_DoBookItems",
                table: "DoReservationBooks",
                column: "BookItemId",
                principalTable: "DoBookItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DoReservationBooks_DoMembers",
                table: "DoReservationBooks",
                column: "MemberId",
                principalTable: "DoMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoBookLendings_BookItems",
                table: "DoBookLendings");

            migrationBuilder.DropForeignKey(
                name: "FK_DoBookLendings_Members",
                table: "DoBookLendings");

            migrationBuilder.DropForeignKey(
                name: "FK_DoMembers_DoMemberGroups",
                table: "DoMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_DoPolicys_DoMembersGroup",
                table: "DoPolicys");

            migrationBuilder.DropForeignKey(
                name: "FK_DoPunishments_DoMembers",
                table: "DoPunishments");

            migrationBuilder.DropForeignKey(
                name: "FK_DoReservationBooks_DoBooks",
                table: "DoReservationBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_DoReservationBooks_DoBookItems",
                table: "DoReservationBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_DoReservationBooks_DoMembers",
                table: "DoReservationBooks");

            migrationBuilder.DropIndex(
                name: "IX_DoReservationBooks_BookItemId",
                table: "DoReservationBooks");

            migrationBuilder.DropIndex(
                name: "IX_DoPolicys_MemberGroupId",
                table: "DoPolicys");

            migrationBuilder.DropColumn(
                name: "BookItemId",
                table: "DoReservationBooks");

            migrationBuilder.DropColumn(
                name: "MemberGroupId",
                table: "DoPolicys");

            migrationBuilder.AddForeignKey(
                name: "FK_DoBookLendings_BookItems",
                table: "DoBookLendings",
                column: "BookItemId",
                principalTable: "DoBookItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoBookLendings_Members",
                table: "DoBookLendings",
                column: "MemberId",
                principalTable: "DoMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoMembers_DoMemberGroups",
                table: "DoMembers",
                column: "MemberGroupId",
                principalTable: "DoMemberGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoPunishments_DoMembers",
                table: "DoPunishments",
                column: "MemberId",
                principalTable: "DoMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoReservationBooks_DoBooks",
                table: "DoReservationBooks",
                column: "BookId",
                principalTable: "DoBooks",
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
    }
}
