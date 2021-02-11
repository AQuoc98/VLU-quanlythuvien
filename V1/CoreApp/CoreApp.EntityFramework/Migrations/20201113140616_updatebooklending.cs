using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class updatebooklending : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BookItemId",
                table: "DoBookLendings",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_DoBookLendings_DoBookItemId",
                table: "DoBookLendings",
                column: "BookItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoBookLendings_DoBookItems_BookItemId",
                table: "DoBookLendings",
                column: "BookItemId",
                principalTable: "DoBookItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoBookLendings_DoBookItems_DoBookItemId",
                table: "DoBookLendings");

            migrationBuilder.DropIndex(
                name: "IX_DoBookLendings_DoBookItemId",
                table: "DoBookLendings");

            migrationBuilder.DropColumn(
                name: "DoBookItemId",
                table: "DoBookLendings");
        }
    }
}
