using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class removenum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropColumn(
                name: "NumberofPages",
                table: "DoBooks");




        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AddColumn<int>(
                name: "NumberofPages",
                table: "DoBooks",
                nullable: false,
                defaultValue: 0);

        }
    }
}
