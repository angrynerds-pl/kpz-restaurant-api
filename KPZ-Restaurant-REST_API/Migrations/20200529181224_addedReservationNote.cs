using Microsoft.EntityFrameworkCore.Migrations;

namespace KPZ_Restaurant_REST_API.Migrations
{
    public partial class addedReservationNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Reservations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Reservations");
        }
    }
}
