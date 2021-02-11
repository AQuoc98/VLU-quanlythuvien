using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class fk_member_memberGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<Guid>(
                name: "MemberGroupId",
                table: "DoMembers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_DoMembers_MemberGroupId",
                table: "DoMembers",
                column: "MemberGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoMembers_DoMemberGroups",
                table: "DoMembers",
                column: "MemberGroupId",
                principalTable: "DoMemberGroups",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoMembers_DoMemberGroups",
                table: "DoMembers");

            migrationBuilder.DropIndex(
                name: "IX_DoMembers_MemberGroupId",
                table: "DoMembers");

            migrationBuilder.DropColumn(
                name: "MemberGroupId",
                table: "DoMembers");

            
        }
    }
}
