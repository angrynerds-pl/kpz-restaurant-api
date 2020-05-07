using Microsoft.EntityFrameworkCore.Migrations;

namespace KPZ_Restaurant_REST_API.Migrations
{
    public partial class RoomDeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Rooms_RoomId",
                table: "Tables");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Rooms_RoomId",
                table: "Tables",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Rooms_RoomId",
                table: "Tables");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Rooms_RoomId",
                table: "Tables",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
