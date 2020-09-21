using Microsoft.EntityFrameworkCore.Migrations;

namespace Bike_EShop.Infrastructure.Migrations
{
    public partial class AddingRegistrationNumberColumnToProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BikeRegistrationNumber",
                table: "Products",
                maxLength: 8,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BikeRegistrationNumber",
                table: "Products");
        }
    }
}
