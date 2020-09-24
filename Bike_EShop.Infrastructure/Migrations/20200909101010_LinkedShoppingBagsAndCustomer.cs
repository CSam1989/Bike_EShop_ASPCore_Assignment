using Microsoft.EntityFrameworkCore.Migrations;

namespace Bike_EShop.Infrastructure.Migrations
{
    public partial class LinkedShoppingBagsAndCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "ShoppingBags",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingBags_CustomerId",
                table: "ShoppingBags",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingBags_Customers_CustomerId",
                table: "ShoppingBags",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingBags_Customers_CustomerId",
                table: "ShoppingBags");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingBags_CustomerId",
                table: "ShoppingBags");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "ShoppingBags");
        }
    }
}
