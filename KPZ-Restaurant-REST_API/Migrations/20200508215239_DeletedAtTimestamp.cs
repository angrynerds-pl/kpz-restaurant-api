using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KPZ_Restaurant_REST_API.Migrations
{
    public partial class DeletedAtTimestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Tables",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Rooms",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Reservations",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(6, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "OrderedProducts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Categories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "OrderedProducts");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Categories");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6, 2)");
        }
    }
}
