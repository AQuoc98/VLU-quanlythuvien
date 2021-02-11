using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class updatebookitem2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BookId",
                table: "DoBookItems",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_DoBookItems_DoBookId",
                table: "DoBookItems",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoBookItems_DoBooks_BookId",
                table: "DoBookItems",
                column: "BookId",
                principalTable: "DoBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoBookItems_DoBooks_DoBookId",
                table: "DoBookItems");

            migrationBuilder.DropIndex(
                name: "IX_DoBookItems_DoBookId",
                table: "DoBookItems");

            migrationBuilder.DropColumn(
                name: "DoBookId",
                table: "DoBookItems");
        }
    }
}
