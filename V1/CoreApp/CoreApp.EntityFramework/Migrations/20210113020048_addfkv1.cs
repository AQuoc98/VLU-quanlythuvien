using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class addfkv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpectDay",
                table: "DoReservationBooks");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DoReservationBooks");

            migrationBuilder.DropColumn(
                name: "finishPunishDate",
                table: "DoPunishments");

            migrationBuilder.DropColumn(
                name: "startPunishDate",
                table: "DoPunishments");

            migrationBuilder.DropColumn(
                name: "numberOfPunishDate",
                table: "DoPolicys");

            migrationBuilder.DropColumn(
                name: "PunishDate",
                table: "DoBookLendings");

            migrationBuilder.RenameColumn(
                name: "numberÒfDueDate",
                table: "DoPolicys",
                newName: "numberOfDueDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpectedDay",
                table: "DoReservationBooks",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReservationDate",
                table: "DoReservationBooks",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PunishDate",
                table: "DoPunishments",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "DoPolicys",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "DoPolicys",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "DoPolicys",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "DoPolicys",
                type: "datetime",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReturnDate",
                table: "DoBookLendings",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "DoBookLendings",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "BorrowedDate",
                table: "DoBookLendings",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpectedDay",
                table: "DoReservationBooks");

            migrationBuilder.DropColumn(
                name: "ReservationDate",
                table: "DoReservationBooks");

            migrationBuilder.DropColumn(
                name: "PunishDate",
                table: "DoPunishments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DoPolicys");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "DoPolicys");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DoPolicys");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "DoPolicys");

            migrationBuilder.RenameColumn(
                name: "numberOfDueDate",
                table: "DoPolicys",
                newName: "numberÒfDueDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpectDay",
                table: "DoReservationBooks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "DoReservationBooks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "finishPunishDate",
                table: "DoPunishments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "startPunishDate",
                table: "DoPunishments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "numberOfPunishDate",
                table: "DoPolicys",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReturnDate",
                table: "DoBookLendings",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "DoBookLendings",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BorrowedDate",
                table: "DoBookLendings",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddColumn<DateTime>(
                name: "PunishDate",
                table: "DoBookLendings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
