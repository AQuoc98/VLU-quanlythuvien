using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class updatebookitemv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            

           

            migrationBuilder.DropColumn(
                name: "BorrowedDate",
                table: "DoBookItems");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "DoBookItems");

            migrationBuilder.AddColumn<Guid>(
                name: "BookLendingId",
                table: "DoBookItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoBookItems_BookLendingId",
                table: "DoBookItems",
                column: "BookLendingId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoBookItems_BookLendings",
                table: "DoBookItems",
                column: "BookLendingId",
                principalTable: "DoBookLendings",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoBookItems_BookLendings",
                table: "DoBookItems");

            migrationBuilder.DropIndex(
                name: "IX_DoBookItems_BookLendingId",
                table: "DoBookItems");

            migrationBuilder.DropColumn(
                name: "BookLendingId",
                table: "DoBookItems");

         

            migrationBuilder.AddColumn<DateTime>(
                name: "BorrowedDate",
                table: "DoBookItems",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "DoBookItems",
                type: "datetime",
                nullable: true);

            
            
        }
    }
}
