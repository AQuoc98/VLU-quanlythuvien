using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class adddatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.CreateTable(
                name: "DoBookItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Barcode = table.Column<string>(maxLength: 20, nullable: true),
                    IsReferenceOnly = table.Column<bool>(nullable: false),
                    BorrowedDate = table.Column<DateTime>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    Status = table.Column<short>(nullable: false),
                    PublicationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoBookItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoBookLendings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BorrowedDate = table.Column<DateTime>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    ReturnDate = table.Column<DateTime>(nullable: false),
                    PunishDate = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoBookLendings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoMemberGroups",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoMemberGroups", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DoMembers",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Address = table.Column<string>(maxLength: 500, nullable: true),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    MemberCode = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoMembers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DoPolicys",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BookNumber = table.Column<int>(nullable: false),
                    numberOfPunishDate = table.Column<int>(nullable: false),
                    numberÒfDueDate = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoPolicys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoPunishments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Reason = table.Column<string>(maxLength: 100, nullable: true),
                    startPunishDate = table.Column<DateTime>(nullable: false),
                    finishPunishDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoPunishments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoRacks",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    LocationIndentifier = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoRacks", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DoReservationBooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    ExpectDay = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoReservationBooks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoBookItems");

            migrationBuilder.DropTable(
                name: "DoBookLendings");

            migrationBuilder.DropTable(
                name: "DoMemberGroups");

            migrationBuilder.DropTable(
                name: "DoMembers");

            migrationBuilder.DropTable(
                name: "DoPolicys");

            migrationBuilder.DropTable(
                name: "DoPunishments");

            migrationBuilder.DropTable(
                name: "DoRacks");

            migrationBuilder.DropTable(
                name: "DoReservationBooks");

          
        }
    }
}
