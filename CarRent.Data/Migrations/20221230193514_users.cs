using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CarRent.Data.Migrations
{
    public partial class users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.Sql("INSERT INTO \"Users\"(\"Email\", \"Password\", \"IsAdmin\", \"Name\", \"LastName\") VALUES ('admin@admin.com', '072c907f9be04b7e2a01c143440e7627', true, 'Admin', 'Adminowski');");
            migrationBuilder.Sql("INSERT INTO \"Users\"(\"Email\", \"Password\", \"IsAdmin\", \"Name\", \"LastName\") VALUES ('user@user.com', '072c907f9be04b7e2a01c143440e7627', false, 'User', 'Userowski');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
