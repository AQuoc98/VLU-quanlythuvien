using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class dropdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoBooks");

            migrationBuilder.DropTable(
                name: "DoAuthors");

            migrationBuilder.DropTable(
                name: "DoCatalogs");

            migrationBuilder.DropTable(
                name: "DoPublishiers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoAuthors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoAuthors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoCatalogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoCatalogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoPublishiers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoPublishiers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoBooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false),
                    CatalogId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ISBN = table.Column<string>(maxLength: 30, nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Language = table.Column<string>(maxLength: 500, nullable: true),
                    NumberofPages = table.Column<int>(nullable: false),
                    PublishierId = table.Column<Guid>(nullable: false),
                    Subject = table.Column<string>(maxLength: 500, nullable: true),
                    Title = table.Column<string>(maxLength: 500, nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoBooks_DoAuthors",
                        column: x => x.AuthorId,
                        principalTable: "DoAuthors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoBooks_DoCatalogs",
                        column: x => x.CatalogId,
                        principalTable: "DoCatalogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoBooks_DoPublishiers",
                        column: x => x.PublishierId,
                        principalTable: "DoPublishiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoBooks_AuthorId",
                table: "DoBooks",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoBooks_CatalogId",
                table: "DoBooks",
                column: "CatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_DoBooks_PublishierId",
                table: "DoBooks",
                column: "PublishierId");
        }
    }
}
