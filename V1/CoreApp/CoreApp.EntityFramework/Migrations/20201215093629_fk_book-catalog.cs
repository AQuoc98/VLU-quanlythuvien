using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class fk_bookcatalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AddColumn<Guid>(
                name: "CatalogId",
                table: "DoBooks",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_DoBooks_CatalogId",
                table: "DoBooks",
                column: "CatalogId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoBooks_DoCatalogs",
                table: "DoBooks",
                column: "CatalogId",
                principalTable: "DoCatalogs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoBooks_DoCatalogs",
                table: "DoBooks");

            migrationBuilder.DropIndex(
                name: "IX_DoBooks_CatalogId",
                table: "DoBooks");

            migrationBuilder.DropColumn(
                name: "CatalogId",
                table: "DoBooks");

            
        }
    }
}
