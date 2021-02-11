using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class updaterefer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoBookItems_DoBooks",
                table: "DoBookItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DoBookItems_DoFormats",
                table: "DoBookItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DoBookItems_DoRacks",
                table: "DoBookItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DoBooks_DoAuthors",
                table: "DoBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_DoBooks_DoCatalogs",
                table: "DoBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_DoBooks_DoPublishiers",
                table: "DoBooks");

            migrationBuilder.AddForeignKey(
                name: "FK_DoBookItems_DoBooks",
                table: "DoBookItems",
                column: "BookId",
                principalTable: "DoBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DoBookItems_DoFormats",
                table: "DoBookItems",
                column: "FormatId",
                principalTable: "DoFormats",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DoBookItems_DoRacks",
                table: "DoBookItems",
                column: "RackId",
                principalTable: "DoRacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DoBooks_DoAuthors",
                table: "DoBooks",
                column: "AuthorId",
                principalTable: "DoAuthors",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DoBooks_DoCatalogs",
                table: "DoBooks",
                column: "CatalogId",
                principalTable: "DoCatalogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DoBooks_DoPublishiers",
                table: "DoBooks",
                column: "PublishierId",
                principalTable: "DoPublishiers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoBookItems_DoBooks",
                table: "DoBookItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DoBookItems_DoFormats",
                table: "DoBookItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DoBookItems_DoRacks",
                table: "DoBookItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DoBooks_DoAuthors",
                table: "DoBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_DoBooks_DoCatalogs",
                table: "DoBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_DoBooks_DoPublishiers",
                table: "DoBooks");

            migrationBuilder.AddForeignKey(
                name: "FK_DoBookItems_DoBooks",
                table: "DoBookItems",
                column: "BookId",
                principalTable: "DoBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoBookItems_DoFormats",
                table: "DoBookItems",
                column: "FormatId",
                principalTable: "DoFormats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoBookItems_DoRacks",
                table: "DoBookItems",
                column: "RackId",
                principalTable: "DoRacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoBooks_DoAuthors",
                table: "DoBooks",
                column: "AuthorId",
                principalTable: "DoAuthors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoBooks_DoCatalogs",
                table: "DoBooks",
                column: "CatalogId",
                principalTable: "DoCatalogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoBooks_DoPublishiers",
                table: "DoBooks",
                column: "PublishierId",
                principalTable: "DoPublishiers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
