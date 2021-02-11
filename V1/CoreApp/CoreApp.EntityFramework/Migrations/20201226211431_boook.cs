using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class boook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoBooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ISBN = table.Column<string>(maxLength: 30, nullable: true),
                    Title = table.Column<string>(maxLength: 500, nullable: true),
                    Subject = table.Column<string>(maxLength: 500, nullable: true),
                    Language = table.Column<string>(maxLength: 500, nullable: true),
                    NumberofPages = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoBooks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoBooks");
        }
    }
}
