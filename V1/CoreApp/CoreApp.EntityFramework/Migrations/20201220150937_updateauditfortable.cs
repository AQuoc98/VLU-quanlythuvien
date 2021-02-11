using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class updateauditfortable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "DoReservationBooks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "DoReservationBooks",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "DoReservationBooks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "DoReservationBooks",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "DoRacks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "DoRacks",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "DoRacks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "DoRacks",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "DoPunishments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "DoPunishments",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "DoPunishments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "DoPunishments",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "DoPublishiers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "DoPublishiers",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "DoPublishiers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "DoPublishiers",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "DoMembers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "DoMembers",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "DoMembers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "DoMembers",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "DoMemberGroups",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "DoMemberGroups",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "DoMemberGroups",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "DoMemberGroups",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "DoFormats",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "DoFormats",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "DoFormats",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "DoFormats",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "DoCatalogs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "DoCatalogs",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "DoCatalogs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "DoCatalogs",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "DoBooks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "DoBooks",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "DoBooks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "DoBooks",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "DoBookLendings",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "DoBookLendings",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "DoBookLendings",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "DoBookLendings",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "DoBookItems",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "DoBookItems",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "DoBookItems",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "DoBookItems",
                type: "datetime",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DoReservationBooks");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "DoReservationBooks");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DoReservationBooks");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "DoReservationBooks");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DoRacks");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "DoRacks");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DoRacks");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "DoRacks");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DoPunishments");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "DoPunishments");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DoPunishments");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "DoPunishments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DoPublishiers");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "DoPublishiers");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DoPublishiers");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "DoPublishiers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DoMembers");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "DoMembers");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DoMembers");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "DoMembers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DoMemberGroups");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "DoMemberGroups");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DoMemberGroups");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "DoMemberGroups");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DoFormats");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "DoFormats");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DoFormats");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "DoFormats");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DoCatalogs");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "DoCatalogs");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DoCatalogs");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "DoCatalogs");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DoBooks");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "DoBooks");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DoBooks");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "DoBooks");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DoBookLendings");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "DoBookLendings");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DoBookLendings");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "DoBookLendings");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DoBookItems");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "DoBookItems");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DoBookItems");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "DoBookItems");
        }
    }
}
