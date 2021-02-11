using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class addFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MemberGroupId",
                table: "DoMembers",
                nullable: false);

            migrationBuilder.AddColumn<long>(
                name: "MemberId",
                table: "DoLibraryCards",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_DoMembers_DoMemberGroupID",
                table: "DoMembers",
                column: "MemberGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_DoLibraryCards_DoMemberID",
                table: "DoLibraryCards",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoLibraryCards_DoMembers_MemberId",
                table: "DoLibraryCards",
                column: "MemberId",
                principalTable: "DoMembers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoMembers_DoMemberGroups_MemberGroupId",
                table: "DoMembers",
                column: "MemberGroupId",
                principalTable: "DoMemberGroups",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoLibraryCards_DoMembers_MemberId",
                table: "DoLibraryCards");

            migrationBuilder.DropForeignKey(
                name: "FK_DoMembers_DoMemberGroups_MemberGroupId",
                table: "DoMembers");

            migrationBuilder.DropIndex(
                name: "IX_DoMembers_DoMemberGroupID",
                table: "DoMembers");

            migrationBuilder.DropIndex(
                name: "IX_DoLibraryCards_DoMemberID",
                table: "DoLibraryCards");

            migrationBuilder.DropColumn(
                name: "MemberGroupId",
                table: "DoMembers");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "DoLibraryCards");
        }
    }
}
