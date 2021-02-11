using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class dropfkpub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoBooks_DoPublishiers",
                table: "DoBooks");

            migrationBuilder.DropIndex(
                name: "IX_DoBooks_PublishierId",
                table: "DoBooks");

            migrationBuilder.DropColumn(
                name: "PublishierId",
                table: "DoBooks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PublishierId",
                table: "DoBooks",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_DoBooks_PublishierId",
                table: "DoBooks",
                column: "PublishierId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoBooks_DoPublishiers",
                table: "DoBooks",
                column: "PublishierId",
                principalTable: "DoPublishiers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
