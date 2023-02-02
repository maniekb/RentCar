using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRent.Data.Migrations
{
    public partial class CarAdjustments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FuelInTank",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "TankCapacity",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "FuelType",
                table: "Cars",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerDay",
                table: "Cars",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.Sql("UPDATE \"Cars\" SET \"FuelType\" = 0;");
            migrationBuilder.Sql("UPDATE \"Cars\" SET \"PricePerDay\" = 100.00;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FuelType",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "PricePerDay",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "FuelInTank",
                table: "Cars",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TankCapacity",
                table: "Cars",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
