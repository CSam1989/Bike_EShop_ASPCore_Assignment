using Microsoft.EntityFrameworkCore.Migrations;

namespace Bike_EShop.Infrastructure.Migrations
{
    public partial class LinkedProductAndShoppingBagTroughShoppingItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ShoppingItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShoppingBagId",
                table: "ShoppingItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingItems_ProductId",
                table: "ShoppingItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingItems_ShoppingBagId",
                table: "ShoppingItems",
                column: "ShoppingBagId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingItems_Products_ProductId",
                table: "ShoppingItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingItems_ShoppingBags_ShoppingBagId",
                table: "ShoppingItems",
                column: "ShoppingBagId",
                principalTable: "ShoppingBags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingItems_Products_ProductId",
                table: "ShoppingItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingItems_ShoppingBags_ShoppingBagId",
                table: "ShoppingItems");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingItems_ProductId",
                table: "ShoppingItems");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingItems_ShoppingBagId",
                table: "ShoppingItems");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ShoppingItems");

            migrationBuilder.DropColumn(
                name: "ShoppingBagId",
                table: "ShoppingItems");
        }
    }
}
