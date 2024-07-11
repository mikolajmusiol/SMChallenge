using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingTask.Migrations
{
    public partial class Corrected_desk_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Desks_Bookings_BookingId",
                table: "Desks");

            migrationBuilder.DropIndex(
                name: "IX_Desks_BookingId",
                table: "Desks");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Desks");

            migrationBuilder.AddColumn<int>(
                name: "DeskId",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_DeskId",
                table: "Bookings",
                column: "DeskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Desks_DeskId",
                table: "Bookings",
                column: "DeskId",
                principalTable: "Desks",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Desks_DeskId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_DeskId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "DeskId",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Desks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Desks_BookingId",
                table: "Desks",
                column: "BookingId",
                unique: true,
                filter: "[BookingId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Desks_Bookings_BookingId",
                table: "Desks",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id");
        }
    }
}
