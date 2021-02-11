using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class updatefkbiandlending : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AlterColumn<Guid>(
                name: "BookItemId",
                table: "DoBookLendings",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "RackId",
                table: "DoBookItems",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "FormatId",
                table: "DoBookItems",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "BookId",
                table: "DoBookItems",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_DoBookItems_DoBooks",
                table: "DoBookItems",
                column: "BookId",
                principalTable: "DoBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

           

           

            migrationBuilder.AddForeignKey(
                name: "FK_DoBookLendings_BookItems",
                table: "DoBookLendings",
                column: "BookItemId",
                principalTable: "DoBookItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoBookItems_DoBooks",
                table: "DoBookItems");


            migrationBuilder.DropForeignKey(
                name: "FK_DoBookLendings_BookItems",
                table: "DoBookLendings");

            migrationBuilder.AlterColumn<Guid>(
                name: "BookItemId",
                table: "DoBookLendings",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "RackId",
                table: "DoBookItems",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FormatId",
                table: "DoBookItems",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BookId",
                table: "DoBookItems",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            
        }
    }
}
