using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingTask.Migrations
{
    public partial class Added_Name_to_Desks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Desks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Desks");
        }
    }
}
