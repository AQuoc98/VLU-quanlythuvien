using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.EntityFramework.Migrations
{
    public partial class addfkrebo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.AddForeignKey(
                name: "FK_DoReservationBooks_DoBooks",
                table: "DoReservationBooks",
                column: "BookId",
                principalTable: "DoBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoReservationBooks_DoBooks",
                table: "DoReservationBooks");

           
        }
    }
}
