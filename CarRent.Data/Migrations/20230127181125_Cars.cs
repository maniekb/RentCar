using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CarRent.Data.Migrations
{
    public partial class Cars : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Brand = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    YearOfProduction = table.Column<int>(nullable: false),
                    CarClass = table.Column<int>(nullable: false),
                    FuelInTank = table.Column<int>(nullable: false),
                    TankCapacity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.Sql("INSERT INTO \"Cars\"(\"Brand\", \"Model\", \"YearOfProduction\", \"CarClass\", \"FuelInTank\", \"TankCapacity\") VALUES ('Fiat', 'Punto', 2020, 0, 40, 40);");
            migrationBuilder.Sql("INSERT INTO \"Cars\"(\"Brand\", \"Model\", \"YearOfProduction\", \"CarClass\", \"FuelInTank\", \"TankCapacity\") VALUES ('Audi', 'A3', 2019, 1, 40, 50);");
            migrationBuilder.Sql("INSERT INTO \"Cars\"(\"Brand\", \"Model\", \"YearOfProduction\", \"CarClass\", \"FuelInTank\", \"TankCapacity\") VALUES ('Maserati', 'Quattroporte', 2022, 3, 50, 50);");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
