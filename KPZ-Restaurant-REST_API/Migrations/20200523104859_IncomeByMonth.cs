using Microsoft.EntityFrameworkCore.Migrations;

namespace KPZ_Restaurant_REST_API.Migrations
{
    public partial class IncomeByMonth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IncomeByMonth",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantId = table.Column<int>(nullable: false),
                    Month = table.Column<string>(nullable: true),
                    Income = table.Column<decimal>(type: "decimal(9, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeByMonth", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncomeByMonth_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IncomeByMonth_RestaurantId",
                table: "IncomeByMonth",
                column: "RestaurantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IncomeByMonth");
        }
    }
}
